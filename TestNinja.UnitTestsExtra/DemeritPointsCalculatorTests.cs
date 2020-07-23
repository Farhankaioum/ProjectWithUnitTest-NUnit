using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTestsExtra
{
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _demeritPoints;

        [SetUp]
        public void SetUp()
        {
            _demeritPoints = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(0,0)]
        [TestCase(64,0)]
        [TestCase(65,0)]
        [TestCase(66,0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnsDemeritPoint(int speed, int expectedOutput)
        {
            // Arrange


            // Act
            var result = _demeritPoints.CalculateDemeritPoints(speed);

            // Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(310)]
        public void CalculateDemeritPoints_SpeedIsOutOfRange_ThrowArgumentOutOfRangeException(int input)
        {
            // Assert
            Assert.That(() => _demeritPoints.CalculateDemeritPoints(input), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        
    }
}
