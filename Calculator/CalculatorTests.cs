using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Add_EmptyString_Zero()
        {
            var calculator = new Calculator();

            var sum = calculator.Add(string.Empty);
            Assert.AreEqual(0, sum);
        }

        [TestMethod]
        public void Add_One_One()
        {
            var calculator = new Calculator();

            var sum = calculator.Add("1");
            Assert.AreEqual(1, sum);
        }

        [TestMethod]
        public void Add_Two_Two()
        {
            var calculator = new Calculator();

            var sum = calculator.Add("2");
            Assert.AreEqual(2, sum);
        }
    }

    public class Calculator
    {
        public int Add(string numberString)
        {
            if (string.IsNullOrEmpty(numberString))
            {
                return 0;
            }

            return int.Parse(numberString);
        }
    }
}
