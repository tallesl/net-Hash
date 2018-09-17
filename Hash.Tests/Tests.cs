namespace HashLibrary.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Empty()
        {
            var hash = HashedPassword.New(string.Empty);

            Assert.IsTrue(hash.Check(string.Empty));
            Assert.IsFalse(hash.Check(" "));
        }

        [TestMethod]
        public void DefaultLengths()
        {
            var hash = HashedPassword.New("foo");

            Assert.IsTrue(hash.Check("foo"));
            Assert.IsFalse(hash.Check("bar"));
        }

        [TestMethod]
        public void CustomLengths()
        {
            var hashLength = 100;
            var saltLength = 8;

            var hash = HashedPassword.New("foo", hashLength, saltLength);

            Assert.IsTrue(hash.Check("foo"));
            Assert.IsFalse(hash.Check("bar"));

            Assert.AreEqual(hashLength, hash.Hash.Length);
            Assert.AreEqual(saltLength, hash.Salt.Length);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void InvalidString() => new HashedPassword("foo™", "bar").Check("foo");
    }
}
