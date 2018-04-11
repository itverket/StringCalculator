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
        private const string StartCustomSeparatorIndicator = "//";
        private const string EndCustomSeparatorIndicator = "\n"; 

        public int Add(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return 0;
            }

            var delimiters = GetSeparators(expression);
            var numbers = GetNumbers(expression);

            return SumNumbers(numbers, delimiters);
        }

        private bool HasCustomSeparator(string expression)
        {
            return expression.StartsWith(StartCustomSeparatorIndicator);
        }

        private string GetNumbers(string expression)
        {
            if (HasCustomSeparator(expression))
            {
                var indexStartNumbers = expression.IndexOf(EndCustomSeparatorIndicator) + 1;
                return expression.Substring(indexStartNumbers);
            }

            return expression;
        }

        private string[] GetSeparators(string expression)
        {
            if (!HasCustomSeparator(expression))
            {
                return Separators;
            }

            return GetCustomSeparators(expression);
        }

        private string[] GetCustomSeparators(string expression)
        {
            var indexStartNumbers = expression.IndexOf("\n") + 1;
            var separator = expression.Substring(2, indexStartNumbers - 3);
            return new string[] { separator };
        }

        private int SumNumbers(string delimitedNumbers, string[] separators)
        {
            var numbers = delimitedNumbers.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);

            var sum = 0;
            foreach (var number in numbers)
            {
                sum += int.Parse(number);
            }

            return sum;
        }
    }
}
