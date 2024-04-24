using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PT_8
{
    internal class StatusBarManager
    {
        private TextBlock fileAttributesTextBlock;
        public StatusBarManager(TextBlock fileAttributesTextBlock) 
        {
            this.fileAttributesTextBlock = fileAttributesTextBlock;
        }

        public void UpdateStatusBarAttributes(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                fileAttributesTextBlock.Text = "";
                return;
            }

            try
            {
                if (File.Exists(fullPath))
                {
                    FileAttributes attributes = File.GetAttributes(fullPath);
                    string statusText = GetFileAttributesString(attributes);
                    fileAttributesTextBlock.Text = statusText;
                }
                else if (Directory.Exists(fullPath))
                {
                    FileAttributes attributes = File.GetAttributes(fullPath);
                    string statusText = GetDirectoryAttributesString(attributes);
                    fileAttributesTextBlock.Text = statusText;
                }
            }
            catch (Exception ex)
            {
                fileAttributesTextBlock.Text = "";
                MessageBox.Show($"Failed to get file attributes: {ex.Message}");
            }
        }

        private string GetFileAttributesString(FileAttributes attributes)
        {
            string statusText = "";
            statusText += (attributes.HasFlag(FileAttributes.ReadOnly)) ? "R" : "-";
            statusText += (attributes.HasFlag(FileAttributes.Archive)) ? "A" : "-";
            statusText += (attributes.HasFlag(FileAttributes.System)) ? "S" : "-";
            statusText += (attributes.HasFlag(FileAttributes.Hidden)) ? "H" : "-";
            return statusText;
        }

        private string GetDirectoryAttributesString(FileAttributes attributes)
        {
            string statusText = "";
            statusText += (attributes.HasFlag(FileAttributes.ReadOnly)) ? "R" : "-";
            statusText += (attributes.HasFlag(FileAttributes.Archive)) ? "A" : "-";
            statusText += (attributes.HasFlag(FileAttributes.System)) ? "S" : "-";
            statusText += (attributes.HasFlag(FileAttributes.Hidden)) ? "H" : "-";
            statusText += (attributes.HasFlag(FileAttributes.Directory)) ? "D" : "-";
            statusText += (attributes.HasFlag(FileAttributes.Normal)) ? "N" : "-";
            return statusText;
        }

    }
}
