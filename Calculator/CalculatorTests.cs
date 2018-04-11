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

        [TestMethod]
        public void Add_ManyCommaSeparatedNumbers()
        {
            var calculator = new Calculator();

            var sum = calculator.Add("2,3,4,5");
            Assert.AreEqual(14, sum);
        }

        [TestMethod]
        public void Add_ManyMoreCommaSeparatedNumbers()
        {
            var calculator = new Calculator();

            var sum = calculator.Add("2,3,4,5,6,7,8");
            Assert.AreEqual(2+3+4+5+6+7+8, sum);
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
                var numbers = numbersList.Split(Separator);

                var sum = 0;
                foreach(var number in numbers)
                {
                    sum += int.Parse(number);
                }

                return sum;
            }

            return int.Parse(numbersList);
        }
    }
}
