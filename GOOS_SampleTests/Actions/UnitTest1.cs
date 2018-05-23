using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace GOOS_SampleTests.Actions
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetNowString()
        {
            var provider = Substitute.For<TimeProvider>();
            provider.Now.Returns(new DateTime(2018, 05, 23, 14, 27, 10, 567));
            var time = provider.Now;
            Assert.AreEqual("2018/05/23 14:27:10.567", time.ToString("yyyy/MM/dd HH:mm:ss.fff"));
        }
    }
}
