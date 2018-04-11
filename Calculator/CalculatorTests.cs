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

        [TestMethod]
        public void Add_TwoCommaSeparatedNumbers()
        {
            var calculator = new Calculator();

            var sum = calculator.Add("2,3");
            Assert.AreEqual(5, sum);
        }
    }

    public class Calculator
    {
        private const string Separator = ",";

        public int Add(string numbersList)
        {
            if (string.IsNullOrEmpty(numbersList))
            {
                return 0;
            }

            if (numbersList.Contains(Separator))
            {
                var indexSeparator = numbersList.IndexOf(Separator);

                var firstNumber = numbersList.Substring(0, indexSeparator);
                var secondNumber = numbersList.Substring(indexSeparator + 1);
                return int.Parse(firstNumber) + int.Parse(secondNumber);
            }

            return int.Parse(numbersList);
        }
    }
}
