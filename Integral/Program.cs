using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Integral
{
    class Program
    {
        public delegate double Function(double x);

        public static readonly Tuple<string, Function>[] Functions = 
        {
            new Tuple<string, Function>("y = sin(x)", x => Math.Sin(x)),
            new Tuple<string, Function>("y = x^cos(x)", x => Math.Pow(x, Math.Cos(x))),
            new Tuple<string, Function>("y = x^2", x => x * x),
            new Tuple<string, Function>("y = x^3", x => x * x * x),
            new Tuple<string, Function>("y = 3 / (x + 1)", x => 3 / (x + 1))
        };

        static void Main(string[] args)
        {
            // Test gitu
            double start = 0, end = 2;

            Function func;

            while(true)
            {
                for (int i = 0; i < Functions.Length; i++)
                    Console.WriteLine($"{ i }: { Functions[i].Item1 }");
                Console.Write("Enter index of desired function: ");
                try
                {
                    func = Functions[Convert.ToInt32(Console.ReadLine())].Item2;
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("You entered an unsopported number!");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("You enterd number that's out of range!");
                }
            }

            while(true)
            {
                try
                {
                    Console.WriteLine("Enter start");
                    start = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter end");
                    end = Convert.ToDouble(Console.ReadLine());
                    if (start > end)
                        Console.WriteLine("Start cannot be bigger than end");
                    else
                        break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("You entered an unsopported number!");
                }
            }

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
