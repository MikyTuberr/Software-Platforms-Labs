using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_11
{
    internal class NewtonSymbolDelegate
    {
        public delegate long CalculationDelegate(int n, int k);

        public static long CalculateBinomialCoefficient(int n, int k)
        {
            if (k > n) return 0;
            if (k == 0 || k == n) return 1;

            CalculationDelegate numeratorDelegate = new CalculationDelegate(CalculateNumerator);
            CalculationDelegate denominatorDelegate = new CalculationDelegate(CalculateDenominatorWrapper);

            IAsyncResult numeratorResult = numeratorDelegate.BeginInvoke(n, k, null, null);
            IAsyncResult denominatorResult = denominatorDelegate.BeginInvoke(k, 0, null, null); // Dummy second parameter

            numeratorResult.AsyncWaitHandle.WaitOne();
            denominatorResult.AsyncWaitHandle.WaitOne();

            long numerator = numeratorDelegate.EndInvoke(numeratorResult);
            long denominator = denominatorDelegate.EndInvoke(denominatorResult);

            return numerator / denominator;
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

        private static long CalculateDenominatorWrapper(int k, int dummy)
        {
            return CalculateDenominator(k);
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
