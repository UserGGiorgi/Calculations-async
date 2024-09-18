using System;
using System.Threading.Tasks;

namespace Calculations.ConsoleClient
{
    internal static class Program
    {
        private static CancellationTokenSource? cts;

        /// <summary>
        /// Calculates the sum from 1 to n synchronously.
        /// The value of n is set by the user from the console.
        /// The user can change the boundary n during the calculation, which causes the calculation to be restarted,
        /// this should not crash the application.
        /// After receiving the result, be able to continue calculations without leaving the console.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task Main()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter a number (n) to calculate the sum from 1 to n, or 'exit' to quit:");

                    string? input = Console.ReadLine();

                    if (input?.ToLower(System.Globalization.CultureInfo.CurrentCulture) == "exit")
                    {
                        break;
                    }

                    if (int.TryParse(input, out int n) && n > 0)
                    {
                        cts?.CancelAsync();

                        cts = new CancellationTokenSource();

                        var progress = new Progress<(int, long)>(tuple =>
                        {
                            Console.WriteLine($"Progress: {tuple.Item1 * 100}% - Current Sum: {tuple.Item2}");
                        });

                        Task<long> task = Calculator.CalculateSumAsync(n, cts.Token, progress);

                        Console.WriteLine($"Started calculation for n={n}. You can change n at any time.");

                        long result = await task;
                        Console.WriteLine($"The sum from 1 to {n} is: {result}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a positive integer.");
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Calculation was canceled. You can restart with a new value.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            Console.WriteLine("Program terminated.");
        }
    }
    }
