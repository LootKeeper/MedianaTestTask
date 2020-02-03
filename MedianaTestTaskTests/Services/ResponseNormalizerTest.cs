using MedianaTestTask.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedianaTestTaskTests.Services
{
    [TestFixture]
    public class ResponseNormalizerTest
    {
        IResponseNormalizer _normalizer;

        [SetUp]
        public void Setup()
        {
            _normalizer = new ResponseNormalizer();
        }

        [Test]
        public void NormalizeTest_Valid()
        {
            var values = new List<string>()
            {
                "123",
                "...123",
                "...123...",
                "\r\n123\t",
                "123    "
            };

            try
            {
                values.ForEach(value => _normalizer.Normalize(value));
                Assert.IsTrue(true);
            }
            catch(Exception ex)
            {
                Assert.IsTrue(false);
            }            
        }

        [Test]
        public void NormalizeTest_Invalid()
        {
            try
            {
                _normalizer.Normalize("abc");
                Assert.IsTrue(false);
            }
            catch(Exception ex)
            {
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void NormalizeTest_Empty()
        {
            try
            {
                _normalizer.Normalize("");
                Assert.IsTrue(false);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
