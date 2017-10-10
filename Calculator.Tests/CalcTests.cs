using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calculator.Tests
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        [TestCategory("MsTest")]
        public void Add_8_and_4_result_12()
        {
            // Arrange 
            var c = new Calc();

            // Act or Action
            var result = c.Add(8, 4);

            // Assert
            Assert.AreEqual(12, result);
        }
        [TestMethod]
        [TestCategory("MsTest")]
        public void Add_5_and_17_result_22()
        {
            // Arrange 
            var c = new Calc();

            // Act or Action
            var result = c.Add(5, 17);

            // Assert
            Assert.AreEqual(22, result);
        }
        [TestMethod]
        [TestCategory("MsTest")]
        public void Add_0_and_0_result_0()
        {
            var c = new Calc();
            var result = c.Add(0, 0);
            Assert.AreEqual(0, result);
        }
        [TestMethod]
        [TestCategory("MsTest")]
        public void Add_M3_and_M27_result_M30()
        {
            var c = new Calc();
            var result = c.Add(-3, -27);
            Assert.AreEqual(-30, result);
        }
        [TestMethod]
        [TestCategory("MsTest")]
        [ExpectedException(typeof(OverflowException))]
        public void Add_Max_and_1_result_OverflowException()
        {
            var c = new Calc();
            c.Add(int.MaxValue, 1);
        }
        [TestMethod]
        [TestCategory("MsTest")]
        [ExpectedException(typeof(OverflowException))]
        public void Add_Min_and_M1_result_OverflowException()
        {
            var c = new Calc();
            c.Add(int.MinValue, -1);
        }
        [TestMethod]
        [TestCategory("MsTest")]
        public void Add_Max_and_Max_result_OverflowException()
        {
            var c = new Calc();

            try
            {
                c.Add(int.MinValue, -1);
                Assert.Fail("Die erwartete Exception wurde nicht ausgelöst.");
            }
            catch(OverflowException)
            {
            }
        }
    }
}
