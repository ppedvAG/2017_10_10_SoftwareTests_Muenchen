using Microsoft.QualityTools.Testing.Fakes;
using System;
using Xunit;
using Xunit.Abstractions;

namespace Bank.Tests
{
    public class KontoTests
    {
        private readonly ITestOutputHelper output;
        public KontoTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData(null)]
        public void CanCreateInstance(object _)
        { 
            var konto = new Konto();
            Assert.NotNull(konto);
        }
        [Theory]
        [InlineData(null)]
        public void New_konto_has_kontostand_0(object _)
        {
            var konto = new Konto();
            Assert.Equal(0m, konto.Kontostand);
        }
        [Fact]
        public void Einzahlen_80_kontostand_80()
        {
            var konto = new Konto();
            konto.Einzahlen(80m);
            Assert.Equal(80m, konto.Kontostand);
        }
        [Fact]
        public void Einzahlen_80_and_200_kontostand_80()
        {
            var konto = new Konto();
            konto.Einzahlen(80m);
            konto.Einzahlen(200m);
            Assert.Equal(280m, konto.Kontostand);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-100.49375)]
        [InlineData(-10000.45)]
        [InlineData(-934265.2345)]
        public void Einzahlen_negative_values_ArgumentException(decimal value)
        {
            var konto = new Konto();
            Assert.Throws<ArgumentException>(() => konto.Einzahlen(value));
        }
        [Fact]
        public void Einzahlen_8_Abheben_3_Kontostand_5()
        {
            var konto = new Konto();
            konto.Einzahlen(8);
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 10, 2);
                konto.Abheben(3m);
            }
            Assert.Equal(5m, konto.Kontostand);
        }
        [Fact]
        public void Einzahlen_8_Abheben_3_and_2_Kontostand_3()
        {
            var konto = new Konto();
            konto.Einzahlen(8);
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 10, 2);
                konto.Abheben(3m);
                konto.Abheben(2m);
            }
            Assert.Equal(3m, konto.Kontostand);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-100.49375)]
        [InlineData(-10000.45)]
        [InlineData(-934265.2345)]
        public void Abheben_negative_values_ArgumentException(decimal value)
        {
            var konto = new Konto();
            konto.Einzahlen(int.MaxValue);
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 10, 2);
                Assert.Throws<ArgumentException>(() => konto.Abheben(value));
            }
        }
        [Theory]
        [InlineData(0, 1)]
        [InlineData(5, 9)]
        [InlineData(1000, 10000)]
        [InlineData(4, 11)]
        public void Abheben_more_than_kontostand_InvalidOperationExeption(decimal einzahlen, decimal auszahlen)
        {
            var konto = new Konto();
            konto.Einzahlen(einzahlen);
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 10, 2);
                Assert.Throws<InvalidOperationException>(() => konto.Abheben(auszahlen));
            }
        }
        [Fact]
        public void Einzahlen_Max_and_1_OverflowException()
        {
            var konto = new Konto();
            konto.Einzahlen(decimal.MaxValue);

            Assert.Throws<OverflowException>(() => konto.Einzahlen(1));
        }
        [Fact]
        public void Abheben_during_Week_NoProblems()
        {
            var konto = new Konto();
            konto.Einzahlen(10);

            using (ShimsContext.Create())
            {
                // Monday
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 10, 2);
                konto.Abheben(1);

                // Tuesday
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 10, 3);
                konto.Abheben(1);

                // Wednesday
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 10, 4);
                konto.Abheben(1);

                // Thursday
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 10, 5);
                konto.Abheben(1);

                // Friday
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 10, 6);
                konto.Abheben(1);
            }

            Assert.Equal(5, konto.Kontostand);
        }
        [Fact]
        public void Abheben_on_Saturday_InvalidOperationException()
        {
            var konto = new Konto();
            konto.Einzahlen(10);

            using (ShimsContext.Create())
            {
                // Saturday
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 8, 12);
                output.WriteLine($"Sould be Saturday. Is: {DateTime.Now.DayOfWeek}");
                Assert.Throws<InvalidOperationException>(() => konto.Abheben(5));
            }
        }
        [Fact]
        public void Abheben_on_Sunday_InvalidOperationException()
        {
            var konto = new Konto();
            konto.Einzahlen(10);

            using (ShimsContext.Create())
            {
                // Sunday
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2017, 10, 8);
                output.WriteLine($"Sould be Sunday. Is: {DateTime.Now.DayOfWeek}");
                Assert.Throws<InvalidOperationException>(() => konto.Abheben(5));
            }
        }
    }
}
