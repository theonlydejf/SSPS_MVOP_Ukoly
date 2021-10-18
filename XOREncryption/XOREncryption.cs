using System;
using System.IO;
using System.Text;

namespace XOREncryption
{
    public class XOREncryption
    {
        private byte[] key;
        public XOREncryption(string key)
        {
            this.key = Encoding.UTF8.GetBytes(key);
        }

        public void Encrypt(Stream input, Stream output)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int b = input.ReadByte();
                if (b < 0)
                    return;

                output.WriteByte((byte)((byte)b ^ key[i % key.Length]));
            }
        }

        public string Encrypt(string data)
        {
            byte[] bData = Encoding.UTF8.GetBytes(data);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bData.Length; i++)
                sb.Append((char)(bData[i] ^ key[i % key.Length]));

            return sb.ToString();
        }
    }
}
