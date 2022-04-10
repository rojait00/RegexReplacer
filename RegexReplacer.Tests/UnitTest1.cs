using Microsoft.VisualStudio.TestTools.UnitTesting;
using RadzenHelper.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexReplacer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            RegexOptions option = RegexOptions.IgnoreCase;
            List<Enum> values = option.GetEnums();
            Assert.IsTrue(values.Any());
        }

        [TestMethod]
        public void TestMethod2()
        {
            Enum option = RegexOptions.IgnoreCase;
            List<Enum> values = option.GetEnums();
            Assert.IsTrue(values.Any());
        }
    }
}