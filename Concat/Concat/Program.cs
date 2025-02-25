using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concat
{
    internal class Program
    {
        static void Main()
        {
            int[] testCounts = { 1000, 10000, 100000 };
            string logFile = "performance_results.txt";

            using (StreamWriter writer = new StreamWriter(logFile))
            {
                writer.WriteLine("Count, StringConcat (ms), StringBuilder (ms)");
                Console.WriteLine("Count, StringConcat (ms), StringBuilder (ms)");

                foreach (int count in testCounts)
                {
                    long timeConcat = MeasureTime(() => GenerateStringConcat(count));
                    long timeSB = MeasureTime(() => GenerateStringSB(count));

                    string result = $"{count}, {timeConcat}, {timeSB}";
                    writer.WriteLine(result);
                    Console.WriteLine(result);
                    writer.Flush();
                }
            }
            Console.WriteLine("Результаты сохранены в performance_results.txt");
            GenerateReport(logFile);
        }

        static long MeasureTime(Action action)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            action();
            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        static string GenerateStringConcat(int count)
        {
            string result = "";
            for (int i = 0; i < count; i++)
            {
                result += $"Iteration: {i} ";
            }
            return result;
        }

        static string GenerateStringSB(int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append($"Iteration: {i} ");
            }
            return sb.ToString();
        }

        static void GenerateReport(string logFile)
        {
            string reportFile = "performance_report.txt";

            try
            {
                if (!File.Exists(logFile))
                {
                    Console.WriteLine("Ошибка: Файл с результатами не найден.");
                    return;
                }

                var lines = File.ReadAllLines(logFile);
                if (lines.Length == 0)
                {
                    Console.WriteLine("Ошибка: Файл с результатами пуст.");
                    return;
                }

                using (StreamWriter writer = new StreamWriter(reportFile, false, Encoding.UTF8))
                {
                    writer.WriteLine("Отчет о производительности");
                    writer.WriteLine("==========================\n");
                    writer.WriteLine("Код программы:");
                    writer.WriteLine("--------------------------");

                    // Читаем текущий код
                    string sourceCode = File.ReadAllText("StringConcatPerformance.cs"); 
                    writer.WriteLine(sourceCode);
                    writer.WriteLine("\nТаблица результатов:");
                    writer.WriteLine("--------------------------");

                    foreach (var line in lines)
                    {
                        writer.WriteLine(line);
                    }

                    writer.WriteLine("\nВывод:");
                    writer.WriteLine("--------------------------");
                    writer.WriteLine("StringBuilder значительно эффективнее при больших значениях count.");
                }

                Console.WriteLine("Отчет сохранен в performance_report.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при генерации отчета: {ex.Message}");
            }
        }
    }
}
