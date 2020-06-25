using ImmRequest.BusinessLogic.Extentions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.ExtentionsTests
{
    [TestClass]
    public class ExtentionsTests
    {
        [DataTestMethod]
        [DataRow("ThisIsATest", "This Is A Test")]
        [DataRow("thisisatest", "thisisatest")]
        [DataRow("THISISATEST", "THISISATEST")]
        [DataRow("thisIsATest", "this Is A Test")]
        public void SplitByCamelCase(string toSplit, string expectedResult)
        {
            var splittedString = toSplit.SplitByCamelCase();
            Assert.AreEqual(expectedResult, splittedString);
        }
    }
}
