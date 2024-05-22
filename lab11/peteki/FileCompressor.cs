using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace PT_11
{
    internal class FileCompressor
    {
        private const int MaxRetryCount = 3;
        private const int RetryDelayMilliseconds = 1000;

        public static void CompressFiles(string directoryPath)
        {
            var files = Directory.GetFiles(directoryPath);
            var tasks = files.Select(file => Task.Run(() => TryCompressFile(file))).ToArray();

            Task.WhenAll(tasks).Wait();
        }

        private static void TryCompressFile(string file)
        {
            int retryCount = 0;
            bool success = false;

            while (retryCount < MaxRetryCount && !success)
            {
                try
                {
                    string compressedFile = file + ".gz";
                    using (FileStream originalFileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                    using (FileStream compressedFileStream = new FileStream(compressedFile, FileMode.Create))
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                    Console.WriteLine($"Successfully compressed: {file}");
                    success = true;
                }
                catch (IOException ex) when (ex.HResult == -2147024864) // ERROR_SHARING_VIOLATION
                {
                    retryCount++;
                    Console.WriteLine($"File in use, retrying ({retryCount}/{MaxRetryCount}): {file}");
                    Task.Delay(RetryDelayMilliseconds).Wait();
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Access denied to file: {file}. Exception: {ex.Message}");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while compressing file: {file}. Exception: {ex.Message}");
                    break;
                }
            }

            if (!success)
            {
                Console.WriteLine($"Failed to compress file after {MaxRetryCount} attempts: {file}");
            }
        }

        public static void DecompressFiles(string directoryPath)
        {
            var compressedFiles = Directory.GetFiles(directoryPath, "*.gz");
            var tasks = compressedFiles.Select(compressedFile => Task.Run(() => TryDecompressFile(compressedFile))).ToArray();

            Task.WhenAll(tasks).Wait();
        }

        private static void TryDecompressFile(string compressedFile)
        {
            int retryCount = 0;
            bool success = false;

            while (retryCount < MaxRetryCount && !success)
            {
                try
                {
                    string decompressedFile = compressedFile.Substring(0, compressedFile.Length - 3);
                    using (FileStream compressedFileStream = new FileStream(compressedFile, FileMode.Open, FileAccess.Read))
                    using (FileStream decompressedFileStream = new FileStream(decompressedFile, FileMode.Create))
                    using (GZipStream decompressionStream = new GZipStream(compressedFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                    Console.WriteLine($"Successfully decompressed: {compressedFile}");
                    success = true;
                }
                catch (IOException ex) when (ex.HResult == -2147024864) // ERROR_SHARING_VIOLATION
                {
                    retryCount++;
                    Console.WriteLine($"File in use, retrying ({retryCount}/{MaxRetryCount}): {compressedFile}");
                    Task.Delay(RetryDelayMilliseconds).Wait();
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Access denied to file: {compressedFile}. Exception: {ex.Message}");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while decompressing file: {compressedFile}. Exception: {ex.Message}");
                    break;
                }
            }

            if (!success)
            {
                Console.WriteLine($"Failed to decompress file after {MaxRetryCount} attempts: {compressedFile}");
            }
        }
    }
}
