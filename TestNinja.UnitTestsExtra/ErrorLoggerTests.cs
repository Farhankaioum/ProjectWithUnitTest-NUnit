using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Fundamentals;
using TestNinja.Mocking;

namespace TestNinja.UnitTestsExtra
{
    public class ErrorLoggerTests
    {
        private ErrorLogger _logger;
        [SetUp]
        public void SetUp()
        {
            _logger = new ErrorLogger();
        }


        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            // Arrange
            var errorMessage = "a";

            // Act
            _logger.Log(errorMessage);

            // Assert
            Assert.That(_logger.LastError, Is.EqualTo(errorMessage));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidException_ThrowsArgumentNullException(string error)
        {
            // assert
            Assert.That(() => _logger.Log(error), Throws.ArgumentNullException);
            Assert.That(() => _logger.Log(error), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        // Test of Event handler check
        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            // Arrange
            var id = Guid.Empty;
            _logger.ErrorLogged += (sender, args) =>
            {
                id = args;
            };

            // Act
            _logger.Log("a");

            // Assertion
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
