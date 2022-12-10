﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MarketApp.Utills
{
    public static class Encryption
    {
        private static readonly string passPhrase = Environment.UserName;        // can be any string
        private static readonly string saltValue = "adwSZCrweCZfEFhSGsfd342";        // can be any string
        private static readonly string hashAlgorithm = "SHA1";             // can be "MD5"
        private static readonly int passwordIterations = 7;                  // can be any number
        private static readonly string initVector = "~1B2c3D4e5F6g7H8"; // must be 16 bytes
        private static readonly int keySize = 256;                // can be 192 or 128

        public static string Encrypt(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(saltValue);
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            byte[] rgbKey = new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);

            RijndaelManaged managed = new()
            {
                Mode = CipherMode.CBC
            };

            ICryptoTransform transform = managed.CreateEncryptor(rgbKey, bytes);
            MemoryStream stream = new ();
            CryptoStream stream2 = new (stream, transform, CryptoStreamMode.Write);

            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();

            byte[] inArray = stream.ToArray();

            stream.Close();
            stream2.Close();

            return Convert.ToBase64String(inArray);
        }

        public static string Decrypt(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(saltValue);
            byte[] buffer = Convert.FromBase64String(data);
            byte[] rgbKey = new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);

            RijndaelManaged managed = new()
            {
                Mode = CipherMode.CBC
            };
            ICryptoTransform transform = managed.CreateDecryptor(rgbKey, bytes);
            MemoryStream stream = new (buffer);
            CryptoStream stream2 = new (stream, transform, CryptoStreamMode.Read);

            byte[] buffer5 = new byte[buffer.Length];
            int count = stream2.Read(buffer5, 0, buffer5.Length);

            stream.Close();
            stream2.Close();

            return Encoding.UTF8.GetString(buffer5, 0, count);
        }
    }
}
