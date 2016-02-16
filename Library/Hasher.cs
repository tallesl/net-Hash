namespace HashLibrary
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// A password hasher.
    /// </summary>
    public class Hasher
    {
        /// <summary>
        /// Extended ASCII.
        /// </summary>
        private static Encoding Encoding = Encoding.GetEncoding(437);

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
            HashLength = hashLength;
            SaltLength = saltLength;
        }

        /// <summary>
        /// Checks if the given password hash is equal to the given hash.
        /// </summary>
        /// <param name="password">Password to hash</param>
        /// <param name="hashed">Hash to check to</param>
        /// <returns>If the given password hash is equal to the given hash</returns>
        public bool Check(string password, HashedPassword hashed)
        {
            var bytes = Encoding.GetBytes(hashed.Salt);
            return hashed.Hash == HashPassword(password, bytes);
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
            var salt = Encoding.GetString(bytes);
            return new HashedPassword(hash, salt);
        }

        /// <summary>
        /// Salts and then hashes with PBKDF2 the given password.
        /// </summary>
        /// <param name="password">Password to salt then hash</param>
        /// <param name="salt">Salt to be used</param>
        /// <returns>The salted and hashed password</returns>
        private string HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                var bytes = pbkdf2.GetBytes(HashLength);
                return Encoding.GetString(bytes);
            }
        }

        /// <summary>
        /// Generates a random salt.
        /// </summary>
        /// <returns>The generated salt</returns>
        private byte[] GenerateSalt()
        {
            var random = new Random(unchecked((int)DateTime.Now.Ticks));
            var salt = new byte[SaltLength];
            random.NextBytes(salt);
            return salt;
        }
    }
}
