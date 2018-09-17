namespace HashLibrary.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Empty()
        {
            var hasher = new Hasher();
            var hashed = hasher.HashPassword(string.Empty);

            Assert.IsTrue(hasher.Check(string.Empty, hashed));
            Assert.IsFalse(hasher.Check(" ", hashed));

            Assert.AreEqual(hasher.HashLength, hashed.Hash.Length);
            Assert.AreEqual(hasher.SaltLength, hashed.Salt.Length);
        }

        [TestMethod]
        public void DefaultLengths()
        {
            var hasher = new Hasher();
            var hashed = hasher.HashPassword("foo");

            Assert.IsTrue(hasher.Check("foo", hashed));
            Assert.IsFalse(hasher.Check("bar", hashed));

            Assert.AreEqual(hasher.HashLength, hashed.Hash.Length);
            Assert.AreEqual(hasher.SaltLength, hashed.Salt.Length);
        }

        [TestMethod]
        public void CustomLengths()
        {
            var hasher = new Hasher(100, 8);
            var hashed = hasher.HashPassword("foo");

            Assert.IsTrue(hasher.Check("foo", hashed));
            Assert.IsFalse(hasher.Check("bar", hashed));

            Assert.AreEqual(hasher.HashLength, hashed.Hash.Length);
            Assert.AreEqual(hasher.SaltLength, hashed.Salt.Length);
        }
    }
}
