using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace ExtractLogs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var datetimeFormat = @"dd/MMM/yyyy:HH:mm:ss zzzz";
            ExtractLogsUtil.ExtractLogsWithinRangeOfTime(
                @"C:\Users\6104655\source\repos\CSharp101\ExtractLogs\Test\Logs.txt",
                DateTimeOffset.ParseExact(@"02/Nov/2020:17:18:17 -0500", datetimeFormat, CultureInfo.InvariantCulture.DateTimeFormat),
                DateTimeOffset.ParseExact(@"02/Nov/2020:17:18:17 -0500", datetimeFormat, CultureInfo.InvariantCulture.DateTimeFormat),
                @"C:\Users\6104655\source\repos\CSharp101\ExtractLogs\Test\TestResult\ExtractedLogs.txt"
            );
        }
    }

    public class ExtractLogsUtil {
        public static void ExtractLogsWithinRangeOfTime(string inputPath, DateTimeOffset start, DateTimeOffset end, string outputPath) {
            var logs = new List<LogRecord>();
            using (var streamReader = new StreamReader(inputPath)) {
                while (streamReader.Peek() > -1) {
                    var line = streamReader.ReadLine();
                    logs.Add(new LogRecord(line));
                }
            }

            var result = logs.Where(
                r => (DateTimeOffset.Compare(r.CreatedTime, start) >= 0 
                        && DateTimeOffset.Compare(r.CreatedTime, end) <= 0)).OrderBy(r => r.CreatedTime).ToList();

            using (var streamWriter = new StreamWriter(outputPath)) {
                foreach (var record in result) {
                    streamWriter.WriteLine(record.OriginalString);
                }
            }
        }
    }

    public class LogRecord {
        public DateTimeOffset CreatedTime { get; set; }
        public string Message { get; set; }
        public string OriginalString {get; private set;}

        public LogRecord(string stringLiteral) {
            this.OriginalString = stringLiteral;
            var parts = stringLiteral.Trim().Split('|');
            Console.WriteLine(stringLiteral, '\n', parts[0], parts[1]);
            var datetimeFormat = @"dd/MMM/yyyy:HH:mm:ss zzzz";
            this.CreatedTime = DateTimeOffset.ParseExact(parts[0], datetimeFormat, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
            this.Message = parts[1];
        }
    }
}
