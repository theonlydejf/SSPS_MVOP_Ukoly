using System;

namespace XOREncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            XOREncryption encryption = new XOREncryption("ahoj");
            Console.WriteLine(encryption.Encrypt(encryption.Encrypt("beee")));
        }
    }
}
