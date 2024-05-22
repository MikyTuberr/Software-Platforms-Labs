using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_11
{
    internal class NewtonSymbolTask
    {
        public static long CalculateBinomialCoefficient(int n, int k)
        {
            if (k > n) return 0;
            if (k == 0 || k == n) return 1;

            Task<long> numeratorTask = Task.Run(() => CalculateNumerator(n, k));
            Task<long> denominatorTask = Task.Run(() => CalculateDenominator(k));

            Task.WaitAll(numeratorTask, denominatorTask);

            return numeratorTask.Result / denominatorTask.Result;
        }

        private static long CalculateNumerator(int n, int k)
        {
            long result = 1;
            for (int i = 0; i < k; i++)
            {
                result *= (n - i);
            }
            return result;
        }

        private static long CalculateDenominator(int k)
        {
            long result = 1;
            for (int i = 1; i <= k; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
