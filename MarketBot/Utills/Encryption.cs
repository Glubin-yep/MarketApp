using System;
using System.Text;

namespace MarketApp.Utills
{
    public static class Encryption
    {
        private static readonly string passPhrase = Environment.UserName;

        public static string Encrypt(string date)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(date);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] += (byte)passPhrase.Length;
            }

            return Encoding.UTF8.GetString(bytes);
        }

        public static string Decrypt(string date)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(date);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] -= (byte)passPhrase.Length;
            }

            return Encoding.UTF8.GetString(bytes);
        }
    }
}
