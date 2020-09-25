using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository repository)
        {
            if (booking.Status == "Cancelled")
                return string.Empty; 

           // var bookings = repository.GetActiveBookings(booking.Id);

            var bookingsTest = new List<Booking>
            {
                new Booking
                {
                     Id = 2,
                    ArrivalDate = new DateTime(2020, 1, 10, 14, 0, 0),
                    DepartureDate = new DateTime(2020, 1, 15, 10, 0, 0),
                    Reference = "a"
                }
            };
            var overlappingBooking =
                bookingsTest.FirstOrDefault(
                    b =>
                        booking.ArrivalDate < b.DepartureDate &&
                        b.ArrivalDate < booking.DepartureDate);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}