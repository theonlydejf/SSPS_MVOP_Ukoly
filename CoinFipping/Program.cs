using System;
using System.Diagnostics;

namespace CoinFipping
{
    class Program
    {
        /// <summary>
        /// Instance of <see cref="Random"/> used to generate a coin flip
        /// </summary>
        public static readonly Random random = new Random();

        /// <summary>
        /// How many times the coin should be flipped in one round
        /// </summary>
        public const uint FLIP_CNT = 100;
        /// <summary>
        /// How many times the coin should land on the same time in a row, so
        /// the current round should count as succesful
        /// </summary>
        public const uint DESIRED_SAME_FLIP_CNT = 7;

        /// <summary>
        /// From how many rounds the statistic should be calculated
        /// </summary>
        public const uint ROUND_CNT = 10_000_000;

        /// <summary>
        /// Main entry point for the applicaion
        /// </summary>
        static void Main()
        {
            uint succesfulRounds = 0;

            Console.WriteLine("Working...");

            Stopwatch sw = Stopwatch.StartNew();
            // Rounds loop
            for (int round = 0; round < ROUND_CNT; round++)
            {
                bool lastFlipState = FlipCoin();
                int sameFlipCnt = 0;

                // Flipping loop
                for (int flipIdx = 0; flipIdx < FLIP_CNT - 1; flipIdx++)
                {
                    bool flip = FlipCoin();

                    // If the flip was same as the last one -> count, else reset counter
                    if (flip == lastFlipState)
                        sameFlipCnt++;
                    else
                        sameFlipCnt = 0;

                    // If the desired amount of flips is reached -> stop the round (more efficiency)
                    if (sameFlipCnt >= DESIRED_SAME_FLIP_CNT)
                        break;

                    lastFlipState = flip;
                }

                // If the last round was succesful -> count
                if (sameFlipCnt >= DESIRED_SAME_FLIP_CNT)
                    succesfulRounds++;
            }
            sw.Stop();

            Console.WriteLine($"Simulation took {sw.ElapsedMilliseconds}ms");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Calculated chance for having {DESIRED_SAME_FLIP_CNT} same coin flips in row for a series of {FLIP_CNT} flips is {(succesfulRounds / (double)ROUND_CNT) * 100d:F2}%");
            Console.ResetColor();
        }

        /// <summary>
        /// Flips an artificia coin (has a 50% chance to be true and 50% chance to be false)
        /// </summary>
        /// <returns>Tha state of the flipped coin. True if it landed on heads, false if it landed on tails</returns>
        private static bool FlipCoin() => random.NextDouble() > .5;
    }
}
