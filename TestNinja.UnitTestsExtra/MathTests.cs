using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.UnitTestsExtra
{
    public class MathTests
    {
        private Math _math;

        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        public void Add_WhenCalled_ReturnsSumOfArguments()
        {
            // Arrange
            int a = 10;
            int b = 20;
            int result = 30;

            // Act
            var returnResult = _math.Add(a, b);

            // Assert
            Assert.That(returnResult, Is.EqualTo(result));
        }

        [Test]
        public void Max_FirstArgumentIsGreater_ReturnsFirstArgument()
        {
            // Arrange
            int a = 20;
            int b = 10;
            int result = 20;

            // Act
            var returnValue = _math.Max(a, b);

            // Assert
            Assert.That(returnValue, Is.EqualTo(result));
        }

        [Test]
        public void Max_SecondArgumentIsGreater_ReturnsSecondArgument()
        {
            // Arrange
            int a = 10;
            int b = 20;
            int result = 20;

            // Act
            var returnValue = _math.Max(a, b);

            // Assert
            Assert.That(returnValue, Is.EqualTo(result));
        }

        [Test]
        public void Max_TwoArgumentAreSame_ReturnArgument()
        {
            // Arrange
            var a = 10;
            var b = 10;
            var result = 10;

            // Act
            var returnValue = _math.Max(a, b);

            // Assert
            Assert.That(returnValue, Is.EqualTo(result));

        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
           var result = _math.GetOddNumbers(5);

            Assert.That(result, Is.Not.Empty);

            Assert.That(result.Count(), Is.EqualTo(3));

            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] {1, 3, 5}));

            //Assert.That(result, Is.Ordered);
            //Assert.That(result, Is.Unique);


        }
    }
}
