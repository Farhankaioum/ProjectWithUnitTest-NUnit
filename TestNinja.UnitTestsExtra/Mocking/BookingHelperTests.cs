using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.UnitTestsExtra.Mocking
{
    public class BookingHelperTests
    {
        private Booking _existingBooking;
        private Mock<IBookingRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2020, 1, 10),
                DepartureDate = DepartOn(2020, 1, 15),
                Reference = "a"
            };

            _repository = new Mock<IBookingRepository>();

            _repository.Setup(b => b.GetActiveBookings(1))
                .Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishAnExistingBooking_ReturnEmptyString()
        {
            // Arrange

            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate)
            }, _repository.Object);

            // Assert
            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1 )
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year,int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }
        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartBeforeAndFinishInTheMiddleOfAnExistingBooking_ReturnReferenceString()
        {
            // Arrange
            

            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking { 
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate)
            }, _repository.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartBeforeAndFinishAfterAnExistingBooking_ReturnReferenceString()
        {
            // Arrange


            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate, days: 2)
            }, _repository.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartAndFinishInTheMiddleOfAnExistingBooking_ReturnReferenceString()
        {
            // Arrange


            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate, 2)
            }, _repository.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartFinishAfterOfAnExistingBooking_ReturnEmptyString()
        {
            // Arrange


            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, 2)
            }, _repository.Object);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartInTheMiddleAndFinishAfterAnExistingBooking_ReturnReferenceString()
        {
            // Arrange


            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate, days: 1),
                DepartureDate = After(_existingBooking.DepartureDate, days:3)
            }, _repository.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

    }
}
