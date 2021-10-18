using System;

namespace ConsoleEcho
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var ii in args)
                Console.Write(ii);
        }
    }
}
