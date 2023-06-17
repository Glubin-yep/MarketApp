using System.Text;

namespace MarketCore.Utills
{
    public static class Encryption
    {
        private static readonly string passPhrase = Environment.UserName;

        public static string Encrypt(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] += (byte)passPhrase.Length;
            }

            return Convert.ToBase64String(bytes);
        }

        public static string Decrypt(string data)
        {
            byte[] bytes = Convert.FromBase64String(data);

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] -= (byte)passPhrase.Length;
            }

            return Encoding.UTF8.GetString(bytes);
        }

    }
}
