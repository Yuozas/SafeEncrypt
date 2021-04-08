using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SafeEncrypt
{
    public class Hasher
    {
        #region Class Variables

        private HashAlgorithm hashAlgorithm;
        private readonly StringBuilder sb;
        private List<byte> hashBytes;

        #endregion Class Variables

        #region Singleton

        private static readonly Hasher instance = new Hasher();

        static Hasher()
        {
        }

        public static Hasher Instance => instance;

        #endregion Singleton

        private Hasher()
        {
            hashBytes = new List<byte>();
            sb = new StringBuilder();
        }

        private string GetHashed(string input)
        {
            SetBytes(input);
            BytesToString();

            return GetString();
        }

        public string GetHashedMD5(string input)
        {
            hashAlgorithm = MD5.Create();
            return GetHashed(input);
        }

        public string GetHashedSHA1(string input)
        {
            hashAlgorithm = SHA1.Create();
            return GetHashed(input);
        }

        public string GetHashedSHA256(string input)
        {
            hashAlgorithm = SHA256.Create();
            return GetHashed(input);
        }

        public string GetHashedSHA384(string input)
        {
            hashAlgorithm = SHA384.Create();
            return GetHashed(input);
        }

        public string GetHashedSHA512(string input)
        {
            hashAlgorithm = SHA512.Create();
            return GetHashed(input);
        }

        public string GetHashedHMAC(string input)
        {
            try
            {
                hashAlgorithm = HMAC.Create();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return GetHashed(input);
        }

        private void ResetStringBuilder() => sb.Clear();

        private void SetBytes(string input) => hashBytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input)).ToList();

        private void BytesToString()
        {
            ResetStringBuilder();
            for (int i = 0; i < hashBytes.Count; i++)
                sb.Append(hashBytes[i].ToString("x2"));
        }

        private string GetString() => sb.ToString();
    }
}