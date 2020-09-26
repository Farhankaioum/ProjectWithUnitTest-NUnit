using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.UnitTestsExtra.Mocking
{
    public class HouseKeeperServiceTests
    {
        private  Mock<IUnitOfWork> _unitOfWork;
        private  Mock<IStatementGenerator> _statementGenerator;
        private  Mock<IEmailSender> _emailSender;
        private  Mock<IXtraMessageBox> _xtraMessageBox;
        private HouseKeeperService _services;

        private DateTime date = new DateTime(2020, 10, 2);
        private  string _statementFileName;

        private Housekeeper _keeper;

        [SetUp]
        public void SetUp()
        {
            _keeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "something" };

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(un => un.Query<Housekeeper>()).Returns(new List<Housekeeper> {
                _keeper
            }.AsQueryable());

            _statementFileName = "filename";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator.Setup(sg => sg.SaveStatement(_keeper.Oid, _keeper.FullName, date))
                .Returns(() => _statementFileName);

            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();
            _services = new HouseKeeperService(_unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _xtraMessageBox.Object);

           
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            // Arrange

            // Act
            _services.SendStatementEmails(date);

            // Assert
            _statementGenerator.Verify(s => s.SaveStatement(_keeper.Oid, _keeper.FullName, date));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_EmailNullOrEmptyOrWhiteSpace_ShouldNotgenerateStatement(string email)
        {
            // Arrange
            _keeper.Email = email;

            // Act
            _services.SendStatementEmails(date);

            // Assert
            _statementGenerator.Verify(s => s.SaveStatement(_keeper.Oid, _keeper.FullName, date), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_SendingAEmail()
        {
            // Arrange
            
            // Act
            _services.SendStatementEmails(date);

            // Assert
            _emailSender.Verify(s => s.EmailFile(_keeper.Email, _keeper.StatementEmailBody, _statementFileName, It.IsAny<string>()));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileNameNullOrEmptyOrWhiteSpace_ShouldNotSendEmail(string statement)
        {
            // Arrange
            _statementFileName = statement;

            // Act
            _services.SendStatementEmails(date);

            // Assert
            _emailSender.Verify(s => s.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
                Times.Never);
        }


        [Test]
        public void SendStatementEmails_ErrorOccured_ShowMessageBox()
        {
            // Arrange

            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(), 
                It.IsAny<string>()
                )).Throws<Exception>();

            // Act
            _services.SendStatementEmails(date);

            // Assert
            _xtraMessageBox.Verify(s => s.Show(
                It.IsAny<string>(),
                It.IsAny<string>(),
                MessageBoxButtons.OK));
        }
    }
}
