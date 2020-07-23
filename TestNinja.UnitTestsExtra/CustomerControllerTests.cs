using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTestsExtra
{
    public class CustomerControllerTests
    {
        [SetUp]
        public void SetUP()
        {

        }

        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            // Arrange
            var customer = new CustomerController();
            var input = 0;

            // Act
            var result = customer.GetCustomer(input);

            // Assert
            // Only NotFound object
            Assert.That(result, Is.TypeOf<NotFound>());
            // NotFound object or this class derived class object
            Assert.That(result, Is.InstanceOf<NotFound>()) ;
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            // Arrange
            var customer = new CustomerController();
            var Id = 2;

            // Act
            var result = customer.GetCustomer(Id);

            // Assert
            Assert.That(result, Is.TypeOf<Ok>());

            Assert.That(result, Is.InstanceOf<Ok>());
        }
    }
}
