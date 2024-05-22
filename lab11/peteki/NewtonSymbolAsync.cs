using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_11
{
    internal class NewtonSymbolAsync
    {
        public static async Task<long> CalculateBinomialCoefficientAsync(int n, int k)
        {
            if (k > n) return 0;
            if (k == 0 || k == n) return 1;

            var numeratorTask = Task.Run(() => CalculateNumerator(n, k));
            var denominatorTask = Task.Run(() => CalculateDenominator(k));

            await Task.WhenAll(numeratorTask, denominatorTask);

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
