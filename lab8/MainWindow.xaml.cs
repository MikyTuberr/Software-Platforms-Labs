using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace PT_8
{
    public partial class MainWindow : Window
    {
        private TreeViewManager treeViewManager;
        private StatusBarManager statusBarManager;

        public MainWindow()
        {
            InitializeComponent();
            treeViewManager = new TreeViewManager(treeView, txtFileContent);
            statusBarManager = new StatusBarManager(txtFileAttributes);
            treeView.SelectedItemChanged += TreeView_SelectedItemChanged; 
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (treeView.SelectedItem is TreeViewItem selectedItem)
            {
                string fullPath = selectedItem.Tag as string;
                statusBarManager.UpdateStatusBarAttributes(fullPath);
            }
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog() { Description = "Select directory to open" };

            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string selectedPath = dialog.SelectedPath;
                treeViewManager.PopulateTreeView(selectedPath);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
