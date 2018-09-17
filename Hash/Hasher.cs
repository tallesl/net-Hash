namespace HashLibrary
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Cryptography;
    using System.Text;
    using System.Linq;

    /// <summary>
    /// A password hasher.
    /// </summary>
    public class Hasher
    {
        private static readonly char[] _charset = GenerateCharset();

        /// <summary>
        /// Length of the generated hash.
        /// </summary>
        public readonly int HashLength;

        /// <summary>
        /// Length of the generated salt.
        /// </summary>
        public readonly int SaltLength;

        /// <summary>
        /// Ctor.
        /// </summary>
        public Hasher() : this(32, 32) { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="hashLength">Length of the generated hash</param>
        /// <param name="saltLength">Length of the generated salt</param>
        public Hasher(int hashLength, int saltLength)
        {
            if (hashLength <= 0)
                throw new ArgumentOutOfRangeException("hashLength");

            if (saltLength <= 0)
                throw new ArgumentOutOfRangeException("saltLength");

            HashLength = hashLength;
            SaltLength = saltLength;
        }

        /// <summary>
        /// Checks if the given password hash is equal to the given hash.
        /// </summary>
        /// <param name="password">Password to hash</param>
        /// <param name="hashed">Hash to check to</param>
        /// <returns>If the given password hash is equal to the given hash</returns>
        /// <exception cref="ArgumentNullException">If the given password is null</exception>
        /// <exception cref="ArgumentNullException">If the given hashed password is null</exception>
        public bool Check(string password, HashedPassword hashed)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            if (hashed == null)
                throw new ArgumentNullException("hashed");

            return hashed.Hash == HashPassword(password, GetBytes(hashed.Salt));
        }

        /// <summary>
        /// Salts and then hashes with PBKDF2 the given password.
        /// </summary>
        /// <param name="password">Password to salt then hash</param>
        /// <returns>The salted and hashed password</returns>
        public HashedPassword HashPassword(string password)
        {
            var bytes = GenerateSalt();
            var hash = HashPassword(password, bytes);
            var salt = GetString(bytes);

            return new HashedPassword(hash, salt);
        }

        private string HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                return GetString(pbkdf2.GetBytes(HashLength));
            }
        }

        private byte[] GenerateSalt()
        {
            var random = new Random(unchecked((int)DateTime.Now.Ticks));
            var salt = new byte[SaltLength];

            random.NextBytes(salt);

            return salt;
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
