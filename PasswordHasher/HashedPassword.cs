namespace PwdHasher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A hashed password.
    /// </summary>
    public class HashedPassword
    {
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
    }
}
