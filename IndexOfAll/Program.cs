using System;
using System.Collections.Generic;
using System.Linq;

namespace IndexOfAll
{
    class Program
    {
        static void Main(string[] args)
        {
            string word = "ahoj";
            string s = "ahoj a ahoj b ahoj";
            List<int> indexes = new List<int>();
            while(true)
            {
                int idx = s.IndexOf(word, indexes.Count <= 0 ? 0 : indexes.Last() + word.Length);
                if (idx < 0)
                    break;
                indexes.Add(idx);
            }
            indexes.ForEach(x => Console.WriteLine(x));
        }
    }
}
