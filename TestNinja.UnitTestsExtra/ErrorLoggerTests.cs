using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTestsExtra
{
    public class ErrorLoggerTests
    {
        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            // Arrange
            var logger = new ErrorLogger();
            var errorMessage = "a";

            // Act
            logger.Log(errorMessage);

            // Assert
            Assert.That(logger.LastError, Is.EqualTo(errorMessage));
        }
    }
}
