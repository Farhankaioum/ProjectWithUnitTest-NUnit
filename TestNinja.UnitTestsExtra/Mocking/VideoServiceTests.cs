using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.UnitTestsExtra.Mocking
{
    public class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _repository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _repository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            // Arrange
          
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            
            // Act
            var result = _videoService.ReadVideoTitle();

            // Assert
            Assert.That(result, Does.Contain("error").IgnoreCase);
           
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_EmptyUnprocessVideo_ReturnEmptyString()
        {
            // Arrange
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            // Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_ListOfUnprocessVideo_ReturnString()
        {
            // Arrange
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video> {
                new Video {Id = 1, IsProcessed = false, Title="Video-1"},
                new Video {Id = 2, IsProcessed = false, Title="Video-2"}
            }) ;

            // Act 
            var result = _videoService.GetUnprocessedVideosAsCsv();

            // Assert
            Assert.That(result, Does.Contain("1,2"));
        }
        [Test]
        public void GetUnprocessedVideosAsCsv_OneUnprocessVideo_ReturnIdAsString()
        {
            // Arrange
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video {Id = 1, IsProcessed = false, Title="Video-1"}
            });

            // Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            // Assert
            Assert.That(result, Does.Contain("1"));

        }
    }
}
