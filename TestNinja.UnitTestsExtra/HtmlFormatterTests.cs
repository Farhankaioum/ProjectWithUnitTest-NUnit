using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTestsExtra
{
    
   public class HtmlFormatterTests
    {
        [Test]
       public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            // Arrange
            var formatter = new HtmlFormatter();
            var value = "abc";

            // Act
            var result = formatter.FormatAsBold(value);

            // Assert

            // Specific
            Assert.That(result, Is.EqualTo($"<strong>{value}</strong>"));

            // More general
            Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
            Assert.That(result, Does.EndWith("</strong>").IgnoreCase);
            Assert.That(result, Does.Contain(value).IgnoreCase);
        }
    }
}
