using System;
using ArrayFactory.Base;


namespace MVOP_Ukoly
{
    class Program
    {
        static void Main(string[] args)
        {
            IntegerArrayFactory arrayFactory = new IntegerArrayFactory(100);
            var array = arrayFactory.Generate(50);

            Console.Write("Čísla na sudých indexech: ");
            for (int i = 0; i < array.Length; i += 2)
            {
                Console.Write(array[i] + "; ");
            }

            Console.Write("\nČísla na sudých indexech: ");
            for (int i = 1; i < array.Length; i += 2)
            {
                Console.Write(array[i] + "; ");
            }
        }
    }
}
