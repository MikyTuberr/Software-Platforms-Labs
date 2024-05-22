using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace PT_11
{
    internal class FibonacciCalculator
    {
        public static void CalculateFibonacci(int n, BackgroundWorker worker, ProgressBar progressBar, Action<long> onCompleted)
        {
            worker.DoWork += (sender, e) =>
            {
                long a = 0, b = 1;
                for (int i = 1; i <= n; i++)
                {
                    long temp = a;
                    a = b;
                    b = temp + b;

                    worker.ReportProgress((i * 100) / n, b);
                    Thread.Sleep(5);
                }
                e.Result = a;
            };

            worker.ProgressChanged += (sender, e) =>
            {
                progressBar.Value = e.ProgressPercentage;
                progressBar.Update();
            };

            worker.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Error == null && !e.Cancelled)
                {
                    onCompleted((long)e.Result);
                }
            };

            worker.RunWorkerAsync();
        }
    }

}
