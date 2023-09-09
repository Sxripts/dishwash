using System;
using JookoObfuscate.Metrics;

namespace JookoObfuscate.Obfuscation
{
    public class DynamicMetamorphicEngine
    {
        private readonly ObfuscationMetricsTracker metricsTracker;
        private readonly Random rand;

        public DynamicMetamorphicEngine(ObfuscationMetricsTracker metricsTracker)
        {
            this.metricsTracker = metricsTracker;
            this.rand = new Random();
        }

        // Functionally identical methods but syntactically different
        public int DynamicAddition(int a, int b)
        {
            metricsTracker.IncrementMethodsObfuscated();
            switch (rand.Next(3))
            {
                case 0:
                    return SimpleAdd(a, b);
                case 1:
                    return RedundantAdd(a, b);
                case 2:
                    return ObfuscatedAdd(a, b);
                default:
                    throw new Exception("Unexpected random value");
            }
        }

        private int SimpleAdd(int a, int b)
        {
            return a + b;
        }

        private int RedundantAdd(int a, int b)
        {
            // Adding redundant operations
            a *= 1;
            b *= b + 0;
            return a + b;
        }

        private int ObfuscatedAdd(int a, int b)
        {
            // Using bitwise OR for obfuscation
            return (a | b) + (a & b) * 2;
        }
    }
}
