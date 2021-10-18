using System;

namespace BracketValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Enter equation: ");
                string stringEquation = Console.ReadLine();

                BracketValidator validator = new BracketValidator();

                bool isEquationValid = validator.IsSequenceValid(stringEquation);
                if(!isEquationValid)
                {
                    Console.CursorLeft = validator.LastErrorPosition;
                    Console.WriteLine($"^ Error here - { validator.LastErrorMessage }");
                }

                Console.WriteLine($"Equation is { (isEquationValid ? "" : "not ") }valid.");
            }
        }
    }
}
