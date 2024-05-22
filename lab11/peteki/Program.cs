using PT_11;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace peteki
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Test NewtonSymbol with multiple sets
            Console.WriteLine("Testing NewtonSymbolTask:");
            var binomialTestCases = new List<(int n, int k)> { (5, 3), (6, 2), (10, 5) };
            foreach (var (n, k) in binomialTestCases)
            {
                long resultTask = NewtonSymbolTask.CalculateBinomialCoefficient(n, k);
                Console.WriteLine($"Binomial Coefficient ({n}, {k}): {resultTask}");
            }

            // Test NewtonSymbolDelegate
            Console.WriteLine("\nTesting NewtonSymbolDelegate:");
            foreach (var (n, k) in binomialTestCases)
            {
                long resultDelegate = NewtonSymbolDelegate.CalculateBinomialCoefficient(n, k);
                Console.WriteLine($"Binomial Coefficient ({n}, {k}): {resultDelegate}");
            }

            // Test NewtonSymbolAsync
            Console.WriteLine("\nTesting NewtonSymbolAsync:");
            foreach (var (n, k) in binomialTestCases)
            {
                Task<long> resultAsyncTask = NewtonSymbolAsync.CalculateBinomialCoefficientAsync(n, k);
                resultAsyncTask.Wait();
                Console.WriteLine($"Binomial Coefficient ({n}, {k}): {resultAsyncTask.Result}");
            }

            // Create a simple Windows Forms app to test FibonacciCalculator
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form form = new Form();
            ProgressBar progressBar = new ProgressBar() { Dock = DockStyle.Bottom };
            form.Controls.Add(progressBar);
            form.Load += (sender, e) =>
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                FibonacciCalculator.CalculateFibonacci(100, worker, progressBar, result =>
                {
                    Console.WriteLine("Fibonacci result: " + result);
                });
            };

            Console.WriteLine("\nStarting Windows Forms app to test FibonacciCalculator...");
            Application.Run(form);
            

            // Prompt for directory and test FileCompressor
            Console.WriteLine("\nTesting FileCompressor:");
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string directoryPath = folderDialog.SelectedPath;
                    Console.WriteLine($"Compressing files in: {directoryPath}");
                    FileCompressor.CompressFiles(directoryPath);
                    Console.WriteLine("Compression completed.");

                    Thread.Sleep(1000);

                    Console.WriteLine($"Decompressing files in: {directoryPath}");
                    FileCompressor.DecompressFiles(directoryPath);
                    Console.WriteLine("Decompression completed.");
                }
            }
        }
    }
}
