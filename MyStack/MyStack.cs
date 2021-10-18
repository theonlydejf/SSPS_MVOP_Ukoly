using System;
using System.Text;
using System.Linq;

namespace Krcmar.MyStack
{
    public class MyStack<T>
    {
        public int Length { get => length; }
        private int length;

        public int Capacity { get => stack.Length; }

        private T[] stack;

        public MyStack() : this(new T[0]) { }

        public MyStack(int capacity) : this(new T[0], capacity) { }

        public MyStack(T[] initialValue) : this(initialValue, 255) { }

        public MyStack(T[] initialValue, int capacity)
        {
            stack = new T[capacity];
            initialValue.CopyTo(stack, 0);
            length = initialValue.Length;
        }

        public void Push(T value) => stack[length++] = value;
        public T Pop() => length <= 0 ? throw new InvalidOperationException("Popping empty stack!") : stack[--length];
        public T Peek() => stack[length - 1];
        public bool IsEmpty() => length <= 0;
        public void PrintStack()
        {
            foreach (var item in stack.Take(length))
                Console.WriteLine(item);
        }
    }
}
