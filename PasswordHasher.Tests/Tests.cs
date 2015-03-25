using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PwdHasher.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Empty()
        {
            var hasher = new PasswordHasher();
            var hashed = hasher.HashIt(string.Empty);

            Assert.IsTrue(hasher.Check(string.Empty, hashed));
            Assert.IsFalse(hasher.Check(" ", hashed));

            Assert.AreEqual(hasher.HashSize, hashed.Hash.Length);
            Assert.AreEqual(hasher.SaltSize, hashed.Salt.Length);
        }

        [TestMethod]
        public void DefaultLengths()
        {
            var hasher = new PasswordHasher();
            var hashed = hasher.HashIt("foo");

            Assert.IsTrue(hasher.Check("foo", hashed));
            Assert.IsFalse(hasher.Check("bar", hashed));

            Assert.AreEqual(hasher.HashSize, hashed.Hash.Length);
            Assert.AreEqual(hasher.SaltSize, hashed.Salt.Length);
        }

        [TestMethod]
        public void CustomLengths()
        {
            var hasher = new PasswordHasher(100, 8);
            var hashed = hasher.HashIt("foo");

            Assert.IsTrue(hasher.Check("foo", hashed));
            Assert.IsFalse(hasher.Check("bar", hashed));

            Assert.AreEqual(hasher.HashSize, hashed.Hash.Length);
            Assert.AreEqual(hasher.SaltSize, hashed.Salt.Length);
        }
    }
}
