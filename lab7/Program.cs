using System.Runtime.Serialization.Formatters.Binary;

namespace PT_7
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Path was not specified.");
                return;
            }

            string directoryPath = args[0];
            DirectoryInfo directory = new DirectoryInfo(directoryPath);

            if (!directory.Exists)
            {
                Console.WriteLine("Specified directory does not exist");
                return;
            }

            RecursiveDirTraverser traverser = new RecursiveDirTraverser(); 

            Console.WriteLine($"\nContents of {directory.FullName}:");
            traverser.DisplayDirContents(directory);

            FileInfo oldestFile = directory.GetOldestDate(ref directory.GetFiles()[0]);
            Console.WriteLine($"\nOldest content of {directory.FullName}: [name: {oldestFile.Name}, date: {oldestFile.LastWriteTime}]");
            
            SortedList<string, long> sortedDirectoryContents = traverser.LoadSortedDirContents(directory);
            Console.WriteLine($"\nSorted contents of {directory.FullName}:");
            foreach (var entry in sortedDirectoryContents)
            {
                Console.WriteLine($"    {entry.Key} -> {entry.Value}");
            }

            Serializator serializator = new Serializator();

            string filePath = "collection.bin";
            serializator.SerializeCollection(sortedDirectoryContents, filePath);
            Console.WriteLine($"\ncollection has been serialized to file {filePath}");

            var deserializedCollection = serializator.DeserializeCollection(filePath);
            Console.WriteLine("\nDeserialization:");
            foreach (var entry in deserializedCollection)
            {
                Console.WriteLine($"    {entry.Key} -> {entry.Value}");
            }
        }
    }
}