using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Debugging
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Trace.Listeners.Add(new TextWriterTraceListener("log.txt"));
            Trace.AutoFlush = true;

            try
            {
                Debug.WriteLine("Начало работы программы");

                double n1 = Convert.ToDouble(Console.ReadLine());
                double n2 = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Введите операцию (+, -, *, /): ");
                string operation = Console.ReadLine();

                double result = 0;
                switch (operation)
                {
                    case "+":
                        result = n1 + n2;
                        break;
                    case "-":
                        result = n1 - n2;
                        break;
                    case "*":
                        result = n1 * n2;
                        break;
                    case "/":
                        if (n2 == 0)
                        {
                            throw new DivideByZeroException("Ошибка: Деление на ноль.");
                        }
                        result = n1 / n2;
                        break;
                    default:
                        Console.WriteLine("Ошибка: Некорректная операция");
                        Trace.WriteLine("Ошибка: Введена некорректная операция");
                        return;
                }


                if (operation == "*")
                {
                    result += 10;
                }

                Console.WriteLine($"Результат: {result}");
                Debug.WriteLine($"Результат вычислений: {result}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Ошибка: Введено некорректное число.");
                Trace.WriteLine($"FormatException: {ex.Message}\nСтек вызовов: {ex.StackTrace}");
                Debug.WriteLine($"FormatException: {ex.Message}\nСтек вызовов: {ex.StackTrace}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
                Trace.WriteLine($"DivideByZeroException: {ex.Message}\nСтек вызовов: {ex.StackTrace}");
                Debug.WriteLine($"DivideByZeroException: {ex.Message}\nСтек вызовов: {ex.StackTrace}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Trace.WriteLine($"Exception: {ex.Message}\nСтек вызовов: {ex.StackTrace}");
                Debug.WriteLine($"Exception: {ex.Message}\nСтек вызовов: {ex.StackTrace}");
            }
            LoadSystem();

            
        }
        static void LoadSystem()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            long count = 0;
            for (long i = 0; i < 500000000; i++)
            {
                count += i % 2 == 0 ? 1 : -1;
            }

            Console.WriteLine($"Нагрузка завершена. Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
            Trace.WriteLine($"Нагрузка завершена. Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
            Debug.WriteLine($"Нагрузка завершена. Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
        }

       
    }
}
