using System;
using JookoObfuscate.Metrics;

namespace JookoObfuscate.Obfuscation
{
    public class ControlFlowObfuscation
    {
        private readonly ObfuscationMetricsTracker metricsTracker;
        private readonly Random rand;

        public ControlFlowObfuscation(ObfuscationMetricsTracker metricsTracker)
        {
            this.metricsTracker = metricsTracker;
            this.rand = new Random();
        }

        // This function calculates factorial but does so in a confusing way
        public int ObfuscatedFactorial(int n)
        {
            metricsTracker.IncrementControlFlowsObfuscated();
            int result = 1;

            // Nested loops and redundant operations to confuse analysis
            for (int i = 1; i <= n;)
            {
                if (rand.Next(2) == 0)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        result *= i;
                    }
                    i++;
                }
                else
                {
                    i++;
                    result *= (i - 1);
                }
            }

            // Redundant and confusing operations
            return result * 1 + 0;
        }

        // A confusing boolean function
        public bool ObfuscatedIsEven(int n)
        {
            metricsTracker.IncrementControlFlowsObfuscated();

            if (n == 0) return true;
            if (n == 1) return false;

            // Confusing use of recursion
            return ObfuscatedIsEven(n - 2);
        }
    }
}