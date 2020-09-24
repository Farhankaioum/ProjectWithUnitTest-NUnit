using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.UnitTestsExtra.Mocking
{
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _employeeStroge;
        private EmployeeController _employeeController;

        [SetUp]
        public void SetUp()
        {
            _employeeStroge = new Mock<IEmployeeStorage>();
            _employeeController = new EmployeeController(_employeeStroge.Object);
        }

        [Test]
        public void DeleteEmployee_InvalidId_ReturnEmpty()
        {
            // arrange

            // act
            _employeeController.DeleteEmployee(1);

            // assert
            _employeeStroge.Verify(s => s.DeleteEmployee(1));
        }
    }
}
