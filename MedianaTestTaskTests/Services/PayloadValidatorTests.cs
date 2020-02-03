using MedianaTestTask.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedianaTestTaskTests.Services
{
    [TestFixture]
    public class PayloadValidatorTests
    {
        IPayloadValidator _validator;
        [SetUp]
        public void Setup()
        {
            _validator = new PayloadValidator();
        }
        [Test]
        public void IsValidTest_True()
        {
            Assert.IsTrue( _validator.IsValid("...123..."));
            Assert.IsTrue(_validator.IsValid("123\r"));
            Assert.IsTrue(_validator.IsValid("123\t."));
        }
        [Test]
        public void IsValidTest_False()
        {
            Assert.IsFalse(_validator.IsValid("123"));
            Assert.IsFalse(_validator.IsValid("...123"));
            Assert.IsFalse(_validator.IsValid("123   "));
        }
    }
}
