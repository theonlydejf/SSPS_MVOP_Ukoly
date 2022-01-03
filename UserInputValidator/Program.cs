using System;
using System.Linq;

namespace UserInputValidator
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Enter any integer number, that has at least 2 digits:");
            Console.WriteLine(ReadValidatedInput((x => x.Length >= 2), (x => int.TryParse(x, out _))));
            Console.WriteLine("Input is valid!");
            Console.ReadLine();
        }

        static string ReadValidatedInput(params Func<string, bool>[] conditions)
        {
            string input = Console.ReadLine();
            if (conditions.All(x => x(input)))
                return input;

            Console.WriteLine("Input does not satisfy all conditions!");
            return ReadValidatedInput(conditions);
        }
    }
}
