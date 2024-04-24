using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace PT_8
{
    public partial class CreateItemDialog : Window
    {
        public string FileName => txtFileName.Text.Trim();
        public bool IsDirectory => rbDirectory.IsChecked ?? false;

        public CreateItemDialog()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            string fileName = txtFileName.Text.Trim();
            bool isFile = rbFile.IsChecked ?? false;

            if (!ValidateFileName(fileName, isFile))
            {
                return;
            }

            FileAttributes attributes = FileAttributes.Normal;
            if (chkReadOnly.IsChecked == true)
                attributes |= FileAttributes.ReadOnly;
            if (chkArchive.IsChecked == true)
                attributes |= FileAttributes.Archive;
            if (chkHidden.IsChecked == true)
                attributes |= FileAttributes.Hidden;
            if (chkSystem.IsChecked == true)
                attributes |= FileAttributes.System;

            string parentDirectory = Tag as string;
            string fullPath = Path.Combine(parentDirectory, fileName);

            if (CreateFileOrDirectory(fullPath, isFile, attributes))
            {
                DialogResult = true;
            }
        }

        private bool ValidateFileName(string fileName, bool isFile)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("Please enter a file name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (isFile)
            {
                string pattern = @"^[a-zA-Z0-9_\-~]{1,8}\.(txt|php|html)$";
                if (!Regex.IsMatch(fileName, pattern))
                {
                    MessageBox.Show("Invalid file name format. Please use 1-8 characters (letters, numbers, '_', '-', '~') and valid extensions: txt, php, html.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
        }

        private bool CreateFileOrDirectory(string fullPath, bool isFile, FileAttributes attributes)
        {
            try
            {
                if (isFile)
                {
                    using (FileStream fs = File.Create(fullPath))
                    {
                        fs.Close();
                    }
                    File.SetAttributes(fullPath, attributes);
                }
                else
                {
                    Directory.CreateDirectory(fullPath);
                    File.SetAttributes(fullPath, attributes);
                }
                return true;
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Failed to create item: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Security.SecurityException ex)
            {
                MessageBox.Show($"Access denied: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Unauthorized access: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
        }
    }
}
