using System;
using System.Diagnostics;
using JookoObfuscate.Encryption;
using JookoObfuscate.AntiTamper;
using JookoObfuscate.Obfuscation;

namespace JookoObfuscate.Metrics
{
    public class ObfuscationMetricsTracker
    {
        private readonly Stopwatch stopwatch;
        private int methodsObfuscated;
        private int filesEncrypted;
        private int tamperAttempts;

        public ObfuscationMetricsTracker()
        {
            this.stopwatch = new Stopwatch();
            this.methodsObfuscated = 0;
            this.filesEncrypted = 0;
            this.tamperAttempts = 0;
        }

        public void StartTracking()
        {
            stopwatch.Start();
        }

        public void StopTracking()
        {
            stopwatch.Stop();
        }

        public void IncrementMethodsObfuscated()
        {
            methodsObfuscated++;
        }

        public void IncrementFilesEncrypted()
        {
            filesEncrypted++;
        }

        public void IncrementTamperAttempts()
        {
            tamperAttempts++;
        }

        public void LogMetrics()
        {
            Console.WriteLine($"Time Elapsed: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Methods Obfuscated: {methodsObfuscated}");
            Console.WriteLine($"Files Encrypted: {filesEncrypted}");
            Console.WriteLine($"Tamper Attempts: {tamperAttempts}");
        }
    }
}