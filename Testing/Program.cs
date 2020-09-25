using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestNinja.Mocking;

namespace Testing
{
    //public class TestConstraints
    //{
    //    public int id { get; set; }
    //    public string Name { get; set; }
    //    public char Sec { get; set; }
    //    public double price { get; set; }
    //}

    //public class Generics_Constraints<T> where T : class
    //{
    //    public void Show(List<T> sh)
    //    {
    //        foreach (var itemType in sh)
    //        {
    //            Type t = itemType.GetType();

    //            IList<PropertyInfo> props = new List<PropertyInfo>(t.GetProperties());

    //            foreach (var prop in props)
    //            {
    //                object propValue = prop.GetValue(itemType, null);
    //                Console.WriteLine(propValue);
    //            }
    //        }

    //    }

    //}

    class Program
    {
        static void Main(string[] args)
        {

            //var value = BookingHelper.OverlappingBookingsExist(new Booking
            //{
            //    Id = 1,
            //    ArrivalDate = new DateTime(2020, 1, 10, 14, 0, 0).AddDays(-2),
            //    DepartureDate = new DateTime(2020, 1, 10, 14, 0, 0).AddDays(-1)
            //}, new BookingRepository());

            //var value = BookingHelper.OverlappingBookingsExist(new Booking
            //{
            //    Id = 1,
            //    ArrivalDate = new DateTime(2020, 1, 10, 14, 0, 0),
            //    DepartureDate = new DateTime(2020, 1, 12, 14, 0, 0).AddDays(-1)
            //}, new BookingRepository());

            //var value = BookingHelper.OverlappingBookingsExist(new Booking
            //{
            //    Id = 1,
            //    ArrivalDate = new DateTime(2020, 1, 16, 14, 0, 0),
            //    DepartureDate = new DateTime(2020, 1, 18, 14, 0, 0)
            //}, new BookingRepository());

            //var value = BookingHelper.OverlappingBookingsExist(new Booking
            //{
            //    Id = 1,
            //    ArrivalDate = new DateTime(2020, 1, 16, 14, 0, 0),
            //    DepartureDate = new DateTime(2020, 1, 21, 14, 0, 0)
            //}, new BookingRepository());

        }
    }
    
}
