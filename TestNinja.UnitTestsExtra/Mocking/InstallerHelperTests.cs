using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.UnitTestsExtra.Mocking
{
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);

        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            // arrange
            _fileDownloader.Setup(fd => 
                fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            // act
            var result = _installerHelper.DownloadInstaller("customer", "installer");


            // assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void DownloadInstaller_DownloadSuccess_ReturnTrue()
        {
            // arrange
            _fileDownloader.Setup(fd => 
                fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Equals(true);

            // act
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            // assert
            Assert.That(result, Is.True);
        }


    }
}
