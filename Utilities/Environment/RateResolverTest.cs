using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Utilities
{
    [TestFixture]
    public class RateResolverTest
    {
        RateResolver rr;

        [SetUp]
        public void setup()
        {
            rr = new RateResolver();
            rr.AddRate("MY_SVR", .05e0, new DateTime(2010, 1, 1));
            rr.AddRate("MY_SVR", .07e0, new DateTime(2011, 1, 1));
        }
        [Test]
        public void simple()
        {
            Assert.AreEqual(.05e0, rr.GetRate("MY_SVR", 0e0, new DateTime(2010, 1, 1)), 1e-5);
            Assert.AreEqual(.05e0, rr.GetRate("MY_SVR", 0e0, new DateTime(2010, 6, 1)), 1e-5);
            Assert.AreEqual(.07e0, rr.GetRate("MY_SVR", 0e0, new DateTime(2011, 1, 1)), 1e-5);
            Assert.AreEqual(.07e0, rr.GetRate("MY_SVR", 0e0, new DateTime(2011, 6, 1)), 1e-5);
        }
        [Test, ExpectedException(typeof(ApplicationException))]
        public void tooEarly()
        {
            rr.GetRate("MY_SVR", 0e0, new DateTime(2009, 6, 1));
        }
        [Test]
        public void spreadAddition()
        {
            Assert.AreEqual(.09e0, rr.GetRate("MY_SVR", 0.04e0, new DateTime(2010, 1, 1)), 1e-5);
            Assert.AreEqual(.09e0, rr.GetRate("MY_SVR", 0.04e0, new DateTime(2010, 6, 1)), 1e-5);
            Assert.AreEqual(.11e0, rr.GetRate("MY_SVR", 0.04e0, new DateTime(2011, 1, 1)), 1e-5);
            Assert.AreEqual(.11e0, rr.GetRate("MY_SVR", 0.04e0, new DateTime(2011, 6, 1)), 1e-5);
        }
        [Test]
        public void dbResolver()
        {
            RateResolver rr = RateResolver.DBResolver;
        }
    }
}
