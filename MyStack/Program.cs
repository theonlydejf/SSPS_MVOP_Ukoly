using System;

namespace Krcmar.MyStack
{
    class Program
    {
        static void Main(string[] args)
        {
            MyStack<int> stack = new MyStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Console.WriteLine("--Peek: " + stack.Peek());
            stack.PrintStack();
            Console.WriteLine("--Pop: " + stack.Pop());
            stack.PrintStack();

            MyStack<string> stackS = new MyStack<string>();
            stackS.Push("a");
            stackS.Push("b");
            stackS.Push("c");
            Console.WriteLine("--Peek: " + stackS.Peek());
            stackS.PrintStack();
            Console.WriteLine("--Pop: " + stackS.Pop());
            stackS.PrintStack();
        }
    }
}
