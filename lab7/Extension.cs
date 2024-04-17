using System;
using System.Text;

namespace PT_7
{
    internal static class Extension
    {
        public static FileInfo GetOldestDate(this DirectoryInfo directory, ref FileInfo oldestFile)
        {
            DateTime oldestDate = DateTime.Now;
  
            FileInfo[] files = directory.GetFiles();
            DirectoryInfo[] directories = directory.GetDirectories();
            foreach (var file in files)
            {
                if (file.LastWriteTime < oldestDate)
                {
                    oldestFile = file;
                    oldestDate = file.LastWriteTime;
                }
            }
            foreach (var dir in directories)
            {
                GetOldestDate(dir, ref oldestFile);
            }
            return oldestFile;
        }

        public static string GetDosAttributes(this FileSystemInfo fileSystem)
        {
            FileAttributes attributes = fileSystem.Attributes;

            StringBuilder dosAttributes = new StringBuilder();

            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                dosAttributes.Append("R");
            else
                dosAttributes.Append("-");

            if ((attributes & FileAttributes.Archive) == FileAttributes.Archive)
                dosAttributes.Append("A");
            else
                dosAttributes.Append("-");

            if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                dosAttributes.Append("H");
            else
                dosAttributes.Append("-");

            if ((attributes & FileAttributes.System) == FileAttributes.System)
                dosAttributes.Append("S");
            else
                dosAttributes.Append("-");

            return dosAttributes.ToString();
        }
    }
}
