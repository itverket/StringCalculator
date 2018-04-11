using System;
using System.Collections.Generic;
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

            var sum = calculator.Add("//[;]\n1;2;3");
            Assert.AreEqual(6, sum);
        }

        [TestMethod]
        public void Add_NegativeNumbers_ThrowException()
        {
            var calculator = new Calculator();

            var didThrowException = false;
            try 
            {
                var sum = calculator.Add("-3");
            }
            catch(Exception e)
            {
                didThrowException = true;

                Assert.IsTrue(e.Message.Contains("negatives not allowed"));
                Assert.IsTrue(e.Message.Contains("-3"));
            }

            Assert.IsTrue(didThrowException);
        }

        [TestMethod]
        public void Add_MultipleNegativeNumbers_ThrowException()
        {
            var calculator = new Calculator();

            var didThrowException = false;
            try
            {
                var sum = calculator.Add("-1,2,-3,4");
            }
            catch (Exception e)
            {
                didThrowException = true;

                Assert.IsTrue(e.Message.Contains("negatives not allowed"));
                Assert.IsTrue(e.Message.Contains("-1"));
                Assert.IsFalse(e.Message.Contains("2"));
                Assert.IsTrue(e.Message.Contains("-3"));
                Assert.IsFalse(e.Message.Contains("4"));
            }

            Assert.IsTrue(didThrowException);
        }

        [TestMethod]
        public void Add_NumbersLargerThan1000_ShouldBeIgnored()
        {
            var calculator = new Calculator();

            var sum = calculator.Add("1,2,1001,3");
            Assert.AreEqual(6, sum);
        }

        [TestMethod]
        public void Add_CustomLargeSeparator()
        {
            var calculator = new Calculator();

            var sum = calculator.Add("//[***]\n4***5***6");
            Assert.AreEqual(15, sum);
        }
    }

    public class Calculator
    {
        private static readonly string[] Separators = { ",", "\n" };
        private const string StartCustomSeparatorIndicator = "//[";
        private const string EndCustomSeparatorIndicator = "]\n";
        private const int MaxNumberSupported = 1000;

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
            var indexStartNumbers = expression.IndexOf(EndCustomSeparatorIndicator) + 1;
            var separator = expression.Substring(StartCustomSeparatorIndicator.Length, 
                                                 indexStartNumbers - 1 - StartCustomSeparatorIndicator.Length);
            return new string[] { separator };
        }

        private int SumNumbers(string delimitedNumbers, string[] separators)
        {
            var numberStrings = delimitedNumbers.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var negativeNumbers = new List<int>();

            var sum = 0;
            foreach (var numberString in numberStrings)
            {
                var number = int.Parse(numberString);
                if (number < 0)
                {
                    negativeNumbers.Add(number);
                }
                else if (number > MaxNumberSupported)
                {
                    continue;
                }

                sum += int.Parse(numberString);
            }

            if (negativeNumbers.Count > 0)
            {
                throw new Exception("negatives not allowed: " + string.Concat(negativeNumbers));
            }

            return sum;
        }
    }
}
