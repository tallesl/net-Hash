namespace HashLibrary
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System;

    /// <summary>
    /// A hashed password.
    /// </summary>
    public class HashedPassword
    {
        private static readonly char[] _charset = GenerateCharset();

        /// <summary>
        /// The password hash.
        /// </summary>
        public readonly string Hash;

        /// <summary>
        /// The password salt.
        /// </summary>
        public readonly string Salt;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="hash">The password hash</param>
        /// <param name="salt">The password salt</param>
        public HashedPassword(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }

        /// <summary>
        /// Checks if the given plain text password hash is equal equivalent to this hashed one.
        /// </summary>
        /// <param name="password">Plain text password to check</param>
        /// <returns>True if the given password are equivalent, false otherwise</returns>
        /// <exception cref="ArgumentNullException">If the given password is null</exception>
        public bool Check(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            return Hash == HashPassword(password, GetBytes(Salt), Hash.Length);
        }

        /// <summary>
        /// Salts and then hashes with PBKDF2 the given password.
        /// </summary>
        /// <param name="password">Password to salt then hash</param>
        /// <param name="hashLength">Length of the generated hash</param>
        /// <param name="saltLength">Length of the generated salt</param>
        /// <returns>The salted and hashed password</returns>
        public static HashedPassword New(string password, int hashLength = 32, int saltLength = 32)
        {
            if (hashLength <= 0)
                throw new ArgumentOutOfRangeException("hashLength");

            if (saltLength <= 0)
                throw new ArgumentOutOfRangeException("saltLength");

            var bytes = GenerateSalt(saltLength);
            var hash = HashPassword(password, bytes, hashLength);
            var salt = GetString(bytes);

            return new HashedPassword(hash, salt);
        }

        private static byte[] GenerateSalt(int length)
        {
            var random = new Random(unchecked((int)DateTime.Now.Ticks));
            var salt = new byte[length];

            random.NextBytes(salt);

            return salt;
        }

        private static string HashPassword(string password, byte[] salt, int length)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                return GetString(pbkdf2.GetBytes(length));
            }
        }

        private static string GetString(byte[] bytes) => new string(bytes.Select(b => _charset[b]).ToArray());

        private static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length];

            for (var i = 0; i < str.Length; ++i)
            {
                var c = str[i];
                var index = Array.IndexOf(_charset, c);

                if (index < 0)
                    throw new ArgumentException($"Caught an invalid character: 0x{((int)c).ToString("X2")}.");

                bytes[i] = (byte)index;
            }

            return bytes;
        }

        private static char[] GenerateCharset()
        {
            var max = byte.MaxValue + 1;
            var charset = new char[max];

            for (int i = 0, j = 0; i < max; ++i, ++j)
            {
                while (Char.IsControl((char)j))
                    ++j;

                charset[i] = (char)j;
            }

            return charset;
        }
    }
}
