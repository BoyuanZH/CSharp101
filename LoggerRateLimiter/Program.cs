using System;
using System.Collections.Generic;

namespace LoggerRateLimiter
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            Console.WriteLine(logger.shouldPrintMessage(1, "foo"));
            Console.WriteLine(logger.shouldPrintMessage(2, "bar"));
            Console.WriteLine(logger.shouldPrintMessage(3, "foo"));
            Console.WriteLine(logger.shouldPrintMessage(7, "bar"));
            Console.WriteLine(logger.shouldPrintMessage(11, "foo"));
            // TTFFT
        }
    }

    public class Logger {
        private Dictionary<string, int> memo;
        public Logger() => this.memo = new Dictionary<string, int>(); // error

        /*
         * Identical message cannot be printed within 10 secs.
         * The timestamp is in seconds granularity.
         */
        public bool shouldPrintMessage(int timestamp, string message) {
            if (this.memo.TryGetValue(message, out int lastPrintTime)) { // error
                if (lastPrintTime + 10 <= timestamp) {
                    this.memo[message] = timestamp; // error
                    return true;
                } else {
                    return false;
                }
            }
            this.memo.Add(message, timestamp); // error
            return true;
        }
    }
}
