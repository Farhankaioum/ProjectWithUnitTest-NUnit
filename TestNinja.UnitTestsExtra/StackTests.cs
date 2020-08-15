using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTestsExtra
{
    public class StackTests
    {
        private Stack<string> _stack;

        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<string>();
        }

        // Push method testing 
        [Test]
        [TestCase(null)]
        public void Push_NullArgs_ThrowArgumentNullException(string input)
        {

            // Assert
            Assert.That(() => _stack.Push(input), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("one")]
        public void Push_ValidArgs_AddValueInStack(string value)
        {
            // Act
            _stack.Push(value);

            // Assert
            Assert.That(_stack.Count > 0);
        }

        // Count prop tested
        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            // Assert
            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        // Pop method testing
       

        [Test]
        public void Pop_EmptyStack_ReturnInvalidOperationException()
        {
            // Act

            // Assert
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_WhenCalled_ReturnRemoveValueFromList()
        {
            // Arrange
            _stack.Push("one");
            _stack.Push("two");
            _stack.Push("three");

            // Act
            var result = _stack.Pop();

            // Assert
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.EqualTo("three"));
        }

        [Test]
        public void Pop_stackWithAFewObjects_RemoveObjectOnTheTop()
        {
            // Arrange
            _stack.Push("one");
            _stack.Push("two");
            _stack.Push("three");

            // Act
            var result = _stack.Pop();

            // Assert
            Assert.That(_stack.Count, Is.EqualTo(2));
            Assert.That(result, Is.EqualTo("three"));

        }



        // Peek method testing
        [Test]
        public void Peek_EmptyStack_ReturnInvalidOperationException()
        {
            // Act

            // Assert
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackWithObjects_ReturnObjectOnTopOfTheStack()
        {
            // Act
            _stack.Push("One");
            _stack.Push("two");
            _stack.Push("three");
            var returnValue =_stack.Peek();

            // Assert
            Assert.That(returnValue, Is.Not.Empty);
            Assert.That(returnValue, Is.EqualTo("three"));
        }

        [Test]
        public void Peek_StackWithObjects_DoesNotRemoveTheObjectOnTopOfTheStack()
        {
            // Arrange
            _stack.Push("One");
            _stack.Push("two");
            _stack.Push("three");

            // Act
            _stack.Peek();

            // Assert
            Assert.That(_stack.Count, Is.EqualTo(3));
        }
    }
}
