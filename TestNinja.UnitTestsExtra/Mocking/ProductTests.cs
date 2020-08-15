using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.UnitTestsExtra.Mocking
{
    public class ProductTests
    {
        // Test without mock
        [Test]
        public void GetPrice_IsGoldCustomer_Apply30PercentDiscount()
        {
            var product = new Product { ListPrice = 100 };

            var result = product.GetPrice(new Customer { IsGold = true });

            Assert.That(result, Is.EqualTo(70));
        }

        // Test using mock
        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount()
        {
            var customer = new Mock<ICustomer>();
            customer.Setup(c => c.IsGold).Returns(true); // If needed to setup any type of property of this mocking class/ this class

            var product = new Product { ListPrice = 100 };

            var result = product.GetPrice(customer.Object);

        }

    }
}
