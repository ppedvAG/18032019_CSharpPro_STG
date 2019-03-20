using System;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.Tests
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        public void Calc_Sum_3_and_4_Results_7()
        {
           
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(3, 4);

            //Assert
            Assert.AreEqual(7, result);
        }

        [TestMethod]
        public void Calc_Sum_0_and_0_Results_0()
        {
            //Arrange
            var calc = new Calc();

            //Act
            var result = calc.Sum(0, 0);

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Calc_Sum_MAX_and_1_throws_OverflowException()
        {
            var calc = new Calc();

            Assert.ThrowsException<OverflowException>(() => calc.Sum(int.MaxValue, 1));
        }

        [TestMethod]
        public void Calc_IsWeekend()
        {
            var calc = new Calc();

            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 3, 18);//mo
                Assert.IsFalse(calc.IsWeekend());

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 3, 19);//di
                Assert.IsFalse(calc.IsWeekend());

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 3, 20);//mi
                Assert.IsFalse(calc.IsWeekend());

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 3, 21);//do
                Assert.IsFalse(calc.IsWeekend());

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 3, 22);//fr
                Assert.IsFalse(calc.IsWeekend());

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 3, 23);//sa
                Assert.IsTrue(calc.IsWeekend());
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2019, 3, 24);//so
                Assert.IsTrue(calc.IsWeekend());
            }
        }
    }
}
