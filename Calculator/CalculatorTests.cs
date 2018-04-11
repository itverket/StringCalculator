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

            var sum = calculator.Add("2\n3,4,5,6\n7,8");
            Assert.AreEqual(2+3+4+5+6+7+8, sum);
        }

        [TestMethod]
        public void Add_NewLineAndCommaSeparatedNumbers()
        {
            var calculator = new Calculator();

            var sum = calculator.Add("2,3,4,5,6,7,8");
            Assert.AreEqual(2 + 3 + 4 + 5 + 6 + 7 + 8, sum);
        }

        [TestMethod]
        public void Add_CustomSeparator()
        {
            var calculator = new Calculator();

            var sum = calculator.Add("//;\n1;2;3");
            Assert.AreEqual(6, sum);
        }
    }

    public class Calculator
    {
        private static readonly string[] Separators = { ",", "\n" };

        public int Add(string commaSeparatedNumbers)
        {
            if (string.IsNullOrEmpty(commaSeparatedNumbers))
            {
                return 0;
            }

            var delimiters = GetSeparators(commaSeparatedNumbers);
            var numbers = GetNumbers(commaSeparatedNumbers);

            return SumNumbers(numbers, delimiters);
        }

        private string GetNumbers(string commaSeparatedNumbers)
        {
            if (commaSeparatedNumbers.StartsWith("//"))
            {
                var indexStartNumbers = commaSeparatedNumbers.IndexOf("\n") + 1;
                return commaSeparatedNumbers.Substring(indexStartNumbers);
            }

            return commaSeparatedNumbers;
        }

        private string[] GetSeparators(string commaSeparatedNumbers)
        {
            if (!commaSeparatedNumbers.StartsWith("//"))
            {
                return Separators;
            }

            var indexStartNumbers = commaSeparatedNumbers.IndexOf("\n") + 1;
            var separator = commaSeparatedNumbers.Substring(2, indexStartNumbers - 3);
            return new string[] { separator };
        }

        private int SumNumbers(string commaSeparatedNumbers, string[] separators)
        {
            var numbers = commaSeparatedNumbers.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);

            var sum = 0;
            foreach (var number in numbers)
            {
                sum += int.Parse(number);
            }

            return sum;
        }
    }
}
