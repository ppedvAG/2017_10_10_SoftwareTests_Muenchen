using NUnit.Framework;
using System;

namespace Calculator.NUnitTests
{
    [TestFixture]
    public class CalcTests
    {
        [Test]
        [Category("NUnit")]
        public void Add_8_and_4_result_12()
        {
            var c = new Calc();
            var result = c.Add(8, 4);
            Assert.AreEqual(12, result);
        }
        [Test]
        [Category("NUnit")]
        public void Add_8_and_4_result_12_nunitSyntax()
        {
            var c = new Calc();
            var result = c.Add(8, 4);
            Assert.That(result, Is.EqualTo(12));
        }
        [TestCase(1, 1, 2)]
        [TestCase(6, 1, 7)]
        [TestCase(100, 1000, 1100)]
        [Category("NUnit")]
        public void Add_a_and_a_result(int a, int b, int result)
        {
            var c = new Calc();
            var r = c.Add(a, b);
            Assert.AreEqual(result, r);
        }
        [Test]
        [Category("NUnit")]
        public void Add_0_and_0_result_0()
        {
            var c = new Calc();
            var result = c.Add(0, 0);
            Assert.AreEqual(0, result);
        }
        [Test]
        [Category("NUnit")]
        public void Add_M3_and_M27_result_M30()
        {
            var c = new Calc();
            var result = c.Add(-3, -27);
            Assert.AreEqual(-30, result);
        }
        [Test]
        [Category("NUnit")]
        public void Add_Max_and_1_result_OverflowException()
        {
            var c = new Calc();
            Assert.Throws<OverflowException>(() =>c.Add(int.MaxValue, 1));
        }
        [Test]
        [Category("NUnit")]
        public void Add_Min_and_M1_result_OverflowException()
        {
            var c = new Calc();
            Assert.Throws<OverflowException>(() => c.Add(int.MinValue, -1));
        }
    }
}
