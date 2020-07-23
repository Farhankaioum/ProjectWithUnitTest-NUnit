using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTestsExtra
{
    public class FizzBuzzTesting
    {
        private FizzBuzz _fizzBuzz;

        [SetUp]
        public void SetUp()
        {
            _fizzBuzz = new FizzBuzz();
        }

        [Test]
        [TestCase(15, "FizzBuzz")]
        [TestCase(9, "Fizz")]
        [TestCase(25, "Buzz")]
        [TestCase(11, "11")]
        public void GetOutput_WhenCalled_ReturnsFizzBuzzOrFizzOrBuzzOrSameValueConvertedInString(int input, string output)
        {
            // Arrange


            // Act
            var result = FizzBuzz.GetOutput(input);

            // Assert
            Assert.That(result, Is.EqualTo(output));
        }


    }
}
