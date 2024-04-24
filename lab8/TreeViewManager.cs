using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;

namespace PT_8
{
    internal class TreeViewManager
    {
        private TreeView treeView;
        private TextBox textBox;

        public TreeViewManager(TreeView treeView, TextBox textBox)
        {
            this.treeView = treeView;
            this.textBox = textBox;
            SetupContextMenu();
        }

        private void SetupContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem deleteMenuItem = new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(DeleteItem),
                CommandParameter = null
            };
            contextMenu.Items.Add(deleteMenuItem);

            treeView.ContextMenu = contextMenu;
        }

        private TreeViewItem CreateTreeViewItem(string fullPath, string displayName)
        {
            var item = new TreeViewItem
            {
                Header = displayName,
                Tag = fullPath
            };

            item.MouseRightButtonDown += (sender, e) =>
            {
                if (e.ChangedButton == MouseButton.Right)
                {
                    var treeViewItem = sender as TreeViewItem;
                    treeViewItem.IsSelected = true;
                    SetupContextMenuForItem(treeViewItem);
                }
            };

            return item;
        }

        private void SetupContextMenuForItem(TreeViewItem item)
        {
            ContextMenu contextMenu = new ContextMenu();
            if (Directory.Exists(item.Tag as string))
            {
                MenuItem createMenuItem = new MenuItem
                {
                    Header = "Create",
                    Command = new RelayCommand(CreateItem),
                    CommandParameter = item
                };
                contextMenu.Items.Add(createMenuItem);
            }
            else if (File.Exists(item.Tag as string))
            {
                MenuItem openMenuItem = new MenuItem
                {
                    Header = "Open",
                    Command = new RelayCommand(OpenItem),
                    CommandParameter = item
                };
                contextMenu.Items.Add(openMenuItem);
            }

            MenuItem deleteMenuItem = new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(DeleteItem),
                CommandParameter = item
            };
            contextMenu.Items.Add(deleteMenuItem);

            item.ContextMenu = contextMenu;
        }

        private void AddChildItems(TreeViewItem parentItem, DirectoryInfo directoryInfo)
        {
            try
            {
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    var item = CreateTreeViewItem(directory.FullName, directory.Name);
                    AddChildItems(item, directory);
                    parentItem.Items.Add(item);
                }

                foreach (var file in directoryInfo.GetFiles())
                {
                    var item = CreateTreeViewItem(file.FullName, file.Name);
                    parentItem.Items.Add(item);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PopulateTreeView(string rootDirectory)
        {
            var rootInfo = new DirectoryInfo(rootDirectory);
            var rootItem = CreateTreeViewItem(rootInfo.FullName, rootInfo.Name);

            AddChildItems(rootItem, rootInfo);

            treeView.Items.Clear();
            treeView.Items.Add(rootItem);
        }

        private void CheckReadOnly(string fullPath)
        {
            var attributes = File.GetAttributes(fullPath);

            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                attributes &= ~FileAttributes.ReadOnly;
                File.SetAttributes(fullPath, attributes);
            }
        }

        private void DeleteItem(object parameter)
        {
            if (parameter is TreeViewItem treeViewItem)
            {
                var fullPath = treeViewItem.Tag as string;
                CheckReadOnly(fullPath); 

                if (File.Exists(fullPath))
                {
                    try
                    {
                        File.Delete(fullPath);
                        var parent = treeViewItem.Parent as TreeViewItem;
                        parent?.Items.Remove(treeViewItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete file: {ex.Message}");
                    }
                }
                else if (Directory.Exists(fullPath))
                {
                    try
                    {
                        DeleteDirectoryRecursive(fullPath);
                        Directory.Delete(fullPath, true);
                        var parent = treeViewItem.Parent as TreeViewItem;
                        parent?.Items.Remove(treeViewItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete directory: {ex.Message}");
                    }
                }
            }
        }

        private void DeleteDirectoryRecursive(string path)
        {
            foreach (string directory in Directory.GetDirectories(path))
            {
                CheckReadOnly(path);
                DeleteDirectoryRecursive(directory);
            }

            foreach (string file in Directory.GetFiles(path))
            {
                CheckReadOnly(path);
                File.Delete(file);
            }
        }

        private void CreateItem(object parameter)
        {
            if (parameter is TreeViewItem parentItem)
            {
                var fullPath = parentItem.Tag as string;
                var dialog = new CreateItemDialog();

                dialog.Tag = fullPath;

                if (dialog.ShowDialog() == true)
                {
                    var newItemFullPath = Path.Combine(fullPath, dialog.FileName);
                    var newItem = CreateTreeViewItem(newItemFullPath, dialog.FileName);
                    parentItem.Items.Add(newItem);
                }
            }
        }

        private void OpenItem(object parameter)
        {
            if (parameter is TreeViewItem parentItem)
            {
                try
                {
                    var filePath = parentItem.Tag as string;
                    if (File.Exists(filePath))
                    {
                        string content = File.ReadAllText(filePath);
                        textBox.Text = content;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to open file: {ex.Message}");
                }
            }
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
