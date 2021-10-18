using System;
using System.Collections.Generic;
using ArrayFactory.Core;

namespace ArrayFactory.Base
{
    /// <summary>
    /// Class used to generate random Integer arrays
    /// </summary>
    public class IntegerArrayFactory : ArrayFactory<int>
    {
        /// <summary>
        /// Minimum value which should be generated inside an array
        /// </summary>
        public int MinimumValue { get; set; }
        /// <summary>
        /// Maximum value which should be generated inside an array
        /// </summary>
        public int MaximumValue { get; set; }

        /// <summary>
        /// Instance of System.Random used for generating Random numbers
        /// </summary>
        private Random random = new Random();

        /// <summary>
        /// Initializes a new instance of <see cref="IntegerArrayFactory"/> using a maximum value that should be present in  generated arrays
        /// </summary>
        /// <param name="maxValue">An exclusive upper bound of the numbers that will be generated in the arrays</param>
        public IntegerArrayFactory(int maxValue) : this(0, maxValue) { }

        /// <summary>
        /// Initializes a new instance of <see cref="IntegerArrayFactory"/> using a maximum and minimum values that should be present in  generated arrays
        /// </summary>
        /// <param name="minValue">An inclusive lower bound of the numbers that will be generated in the arrays</param>
        /// <param name="maxValue">An exclusive upper bound of the numbers that will be generated in the arrays</param>
        public IntegerArrayFactory(int minValue, int maxValue) : this(minValue, maxValue, new Random()) { }

        /// <summary>
        /// Initializes a new instance of <see cref="IntegerArrayFactory"/> using a maximum and minimum values that should be present in  generated arrays
        /// </summary>
        /// <param name="minValue">An inclusive lower bound of the numbers that will be generated in the arrays</param>
        /// <param name="maxValue">An exclusive upper bound of the numbers that will be generated in the arrays</param>
        /// <param name="random">An instance of <see cref="Random"/> used for generating Random numbers</param>
        public IntegerArrayFactory(int minValue, int maxValue, Random random)
        {
            MinimumValue = minValue;
            MaximumValue = maxValue;
            this.random = random;
        }

        public int[] Generate(int arrayLength)
        {
            // Checks if the factory is able to generated an array of required length
            if (MaximumValue - MinimumValue < arrayLength)
                throw new ArgumentException("Not enaugh integeres in entered range to fill an array of length " + arrayLength);

            // List which contains numbers already present in the array
            IList<int> usedNumbers = new List<int>();
            // Array which will be filled with random numbers
            int[] array = new int[arrayLength];

            // Fills the array with unique random numbers
            for (int i = 0; i < arrayLength; i++)
            {
                array[i] = GetUniqueRandomNumber(usedNumbers);
            }

            return array;
        }

        private int GetUniqueRandomNumber(IList<int> usedNumbers)
        {
            int number = random.Next(MinimumValue, MaximumValue);

            // Generates random numbers until the number is valid
            while(usedNumbers.Contains(number))
                number = random.Next(MinimumValue, MaximumValue);

            usedNumbers.Add(number);
            return number;
        }
    }
}
