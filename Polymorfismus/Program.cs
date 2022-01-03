using System;

namespace Polymorfismus
{
    class Test
    {
        public int A { get; set; }
        public int B { get; set; }

        public override string ToString()
        {
            return A + "; " + B;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Test t = new Test() { A = 10, B = 5 };
            Console.WriteLine(t);
            Test u = t;
            u.A = 5;
            Console.WriteLine("u-" + u);
            Console.WriteLine("t-" + t);
        }
    }
}
