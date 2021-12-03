using System;
using System.Text.RegularExpressions;

namespace Regex_PhoneNumbersHW
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Regex regex = new Regex(@"(?<national_code>(\(\d{3}\))|(\+\d{3}))?(?<phone_number>( ?\d{3}){3})");
                Console.WriteLine("Napište text ve kterém chcete najít telefonní čísla:");
                string s = Console.ReadLine();
                MatchCollection matches = regex.Matches(s);
                Console.WriteLine($"Nalezeno { matches.Count } shod.");
                foreach (Match match in matches)
                {
                    Console.WriteLine(match.Value);
                    Console.Write("  Tel. číslo: ");
                    Console.WriteLine(match.Groups["phone_number"]);
                    if (match.Groups.Count > 1)
                    {
                        Console.Write("  Nár. předvolba: ");
                        Console.WriteLine(match.Groups["national_code"]);
                    }
                }
            }
        }
    }
}