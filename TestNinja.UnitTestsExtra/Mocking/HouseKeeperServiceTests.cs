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

        private Housekeeper _keeper;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _statementGenerator = new Mock<IStatementGenerator>();
            _emailSender = new Mock<IEmailSender>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();
            _services = new HouseKeeperService(_unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _xtraMessageBox.Object);

            _keeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "something" };

        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            // Arrange
            _unitOfWork.Setup(un => un.Query<Housekeeper>()).Returns(new List<Housekeeper> {
                _keeper
            }.AsQueryable());

            // Act
            _services.SendStatementEmails(date);

            // Assert
            _statementGenerator.Verify(s => s.SaveStatement(1, "b", date));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_EmailNullOrEmptyOrWhiteSpace_ShouldNotgenerateStatement(string email)
        {
            // Arrange
            _keeper.Email = email;
            _unitOfWork.Setup(un => un.Query<Housekeeper>()).Returns(new List<Housekeeper> {
                _keeper
            }.AsQueryable());

            // Act
            _services.SendStatementEmails(date);

            // Assert
            _statementGenerator.Verify(s => s.SaveStatement(1, "b", date), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_SendingAEmail()
        {
            // Arrange
            _unitOfWork.Setup(un => un.Query<Housekeeper>()).Returns(new List<Housekeeper> {
                _keeper
            }.AsQueryable());

            _statementGenerator.Setup(sg => sg.SaveStatement(_keeper.Oid, _keeper.FullName, date)).Returns("ab");

            // Act
            _services.SendStatementEmails(date);

            // Assert
            _emailSender.Verify(s => s.EmailFile(_keeper.Email, _keeper.StatementEmailBody, "ab", It.IsAny<string>()));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileNameNullOrEmptyOrWhiteSpace_ShouldNotSendEmail(string statement)
        {
            // Arrange
            _keeper.Email = statement;
            _unitOfWork.Setup(un => un.Query<Housekeeper>()).Returns(new List<Housekeeper> {
                _keeper
            }.AsQueryable());

            _statementGenerator.Setup(sg => sg.SaveStatement(_keeper.Oid, _keeper.FullName, new DateTime(2020, 10, 2))).Returns("ab");

            // Act
            _services.SendStatementEmails(new DateTime(2020, 10, 2));

            // Assert
            _emailSender.Verify(s => s.EmailFile(_keeper.Email, _keeper.StatementEmailBody, "ab", It.IsAny<string>()), Times.Never);
        }


        [Test]
        public void SendStatementEmails_ErrorOccerd_ShowMessageBox()
        {
            // Arrange
            _unitOfWork.Setup(un => un.Query<Housekeeper>()).Returns(new List<Housekeeper> {
                _keeper
            }.AsQueryable());

            _statementGenerator.Setup(sg => sg.SaveStatement(_keeper.Oid, _keeper.FullName, new DateTime(2020, 10, 2))).Returns("something");

            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(), 
                It.IsAny<string>()
                )).Throws<Exception>();

            // Act
            _services.SendStatementEmails(new DateTime(2020, 10, 2));

            // Assert
            _xtraMessageBox.Verify(s => s.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
    }
}
