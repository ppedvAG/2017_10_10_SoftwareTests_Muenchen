using System.Collections.Generic;
using Xunit;

namespace Calculator.XUnitTests
{
    public class CalcTests
    {
        public CalcTests()
        {
            // Testinitialize
        }
        ~CalcTests()
        {
            // Testcleanup/Teardown
        }

        [Trait("XUnit", "Unit")]
        [Fact]
        public void Add_8_and_4_result_12()
        {
            var c = new Calc();
            var result = c.Add(8, 4);
            Assert.Equal(12, result);
        }
        [Theory]
        [InlineData(5, 9, 14)]
        [InlineData(0, 0, 0)]
        //[InlineData(10, 9, 19, Skip = "alksödfjalösfj2")]
        [InlineData(1234, -4321, -3087)]
        [Trait("XUnit", "Unit")]
        public void Add_a_and_b_result(int a, int b, int result)
        {
            var c = new Calc();
            var r = c.Add(a, b);
            Assert.Equal(result, r);
        }
        [Theory]
        [MemberData("Data", MemberType = typeof(DataSource))]
        public void Add_a_and_b_result_memberdata(int a, int b, int result)
        {
            var c = new Calc();
            var r = c.Add(a, b);
            Assert.Equal(result, r);
        }
        private static class DataSource
        {
            public static IEnumerable<object[]> Data => new[]
            {
                new object[] { 3, 8, 11 },
                new object[] { 4, 8, 12 },
                new object[] { 0, -8, -8 },
                new object[] { 3, 8, 11 }
            };
        }
        [Theory]
        [ExcelData("TestData.xls", "Select * from TestData")]
        public void Add_a_and_b_result_excel(int a, int b, int result)
        { 
            var c = new Calc();
            var r = c.Add(a, b);
            Assert.Equal(result, r);
        }
    }
}
