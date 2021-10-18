using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Integral
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test gitu
            double start = 0, end = 2;

            Func<double, double> func = x => x * x * x;
            double resolutionStart = 1e-2, resolutionStepDown = 1e1, resolutionEnd = 1e-4;
            List<double> results = new List<double>();
            for (double resolution = resolutionStart; resolution >= resolutionEnd; resolution /= resolutionStepDown)
            {
                results.Add(0);
                for (double i = start; i < end; i += resolution)
                    results[results.Count - 1] += func(i) * resolution;
                Console.WriteLine($"Step: {resolution}; Result: {results.Last()}");
            }

            double yMin = func(start), yMax = func(end);
            double testCnt = 1e8;
            int inCnt = 0;
            ThreadSafeRandom rnd = new ThreadSafeRandom();
            Parallel.For(0, (int)testCnt, i =>
            {
                double x = (rnd.Next() * (end - start)) + start;
                double y = (rnd.Next() * (yMax - yMin)) + yMin;
                if (y <= func(x))
                    Interlocked.Increment(ref inCnt);
            });
            double monteCarloResult = (end - start) * (yMax - yMin) * (inCnt / testCnt);
            Console.WriteLine($"Monte Carlo: {monteCarloResult}");
            
        }
    }

    // https://stackoverflow.com/questions/3049467/is-c-sharp-random-number-generator-thread-safe
    public class ThreadSafeRandom
    {
        private static readonly Random _global = new Random();
        [ThreadStatic] private static Random _local;

        public double Next()
        {
            if (_local == null)
            {
                int seed;
                lock (_global)
                {
                    seed = _global.Next();
                }
                _local = new Random(seed);
            }

            return _local.NextDouble();
        }
    }
}
