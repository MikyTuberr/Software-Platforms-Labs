using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_7
{
    internal class RecursiveDirTraverser
    {
        public int CountFilesInDir(DirectoryInfo directory)
        {
            int count = directory.GetFiles().Length;
            DirectoryInfo[] directories = directory.GetDirectories();
            foreach (var dir in directories)
            {
                count += CountFilesInDir(dir);
            }
            return count;
        }

        public void DisplayDirContents(DirectoryInfo directory, string indent = "")
        {
            FileInfo[] files = directory.GetFiles();
            DirectoryInfo[] directories = directory.GetDirectories();
            foreach (var file in files)
            {
                Console.WriteLine($"{indent}{file.Name} -> [size: {file.Length} B, attributes: {file.Attributes.ToString()}, DOS: {file.GetDosAttributes()}]");
            }
            foreach (var dir in directories)
            {
                int totalFilesCount = CountFilesInDir(dir);
                Console.WriteLine($"{indent}{dir.Name} -> [elements: {totalFilesCount}, attributes: {dir.Attributes.ToString()}, DOS: {dir.GetDosAttributes()}]");
                DisplayDirContents(dir, indent + "    ");
            }
        }

        public SortedList<string, long> LoadSortedDirContents(DirectoryInfo directory)
        {
            SortedList<string, long> result = new SortedList<string, long>(new DirectoryComparer());
            FileInfo[] files = directory.GetFiles();
            DirectoryInfo[] directories = directory.GetDirectories();
            foreach (var file in files)
            {
                result.Add(file.Name, file.Length);
            }
            foreach (var dir in directories)
            {
                result.Add(dir.Name, dir.GetFiles().Length);
            }
            return result;
        }
    }
}
