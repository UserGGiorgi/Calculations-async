using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using static Calculations.Calculator;

namespace Calculations.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 3)]
        [TestCase(9, ExpectedResult = 45)]
        [TestCase(10, ExpectedResult = 55)]
        [TestCase(50, ExpectedResult = 1275)]
        [TestCase(100, ExpectedResult = 5050)]
        [TestCase(29231, ExpectedResult = 427240296)]
        [TestCase(100_000, ExpectedResult = 5000050000L)]
        public long CalculateSumTests(int n) => CalculateSum(n);

        [TestCase(0)]
        [TestCase(-1)]
        public void CalculateSumNumberLessOrEqualsZeroThrowArgumentOutOfRangeException(int n)
            => Assert.Throws<ArgumentOutOfRangeException>(() => CalculateSum(n));

        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 3)]
        [TestCase(9, ExpectedResult = 45)]
        [TestCase(10, ExpectedResult = 55)]
        [TestCase(50, ExpectedResult = 1275)]
        [TestCase(100, ExpectedResult = 5050)]
        [TestCase(99997, ExpectedResult = 4999750003)]
        [TestCase(10_000_000, ExpectedResult = 50000005000000L)]
        public async Task<long> CalculateSumAsyncWithCancellationTokenNone(int n) =>
            await CalculateSumAsync(n, CancellationToken.None);

        [TestCase(0)]
        [TestCase(-1)]
        public void CalculateSumAsyncNumberLessOrEqualsZeroThrowArgumentOutOfRangeException(int n)
            => Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                await CalculateSumAsync(n, CancellationToken.None));

        [TestCase(100_000, 5000050000L, false, ExpectedResult = false)]
        [TestCase(10_000_000, 50000005000000L, true, ExpectedResult = true)]
        public async Task<bool> CalculateSumAsyncWithCancellationTokenAndProgress(int n, long expectedSum, bool cancel)
        {
            using var cancelTokenSource = new CancellationTokenSource();

            (int index, long value) lastValue = (0, 0);
            var progress = new Progress<(int, long)>(p => (lastValue.index, lastValue.value) = (p.Item1, p.Item2));
            var calculationTask = CalculateSumAsync(n, cancelTokenSource.Token, progress);

            if (cancel)
            {
                await Task.Delay(10, CancellationToken.None);
                await cancelTokenSource.CancelAsync();
            }

            long sum = 0;
            try
            {
                sum = await calculationTask;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"Current status of task: {calculationTask.Status}.");
                Console.WriteLine($"Current value of sum: {lastValue.Item2} at the index: {lastValue.Item1}.");
                return lastValue.value != expectedSum;
            }

            Console.WriteLine($"Current status of task: {calculationTask.Status}.");
            Console.WriteLine($"Current value of sum: {lastValue.value} at the index: {lastValue.index}.");
            Console.WriteLine($"Current value of sum: {sum}.");

            return sum != expectedSum;
        }
    }
}
