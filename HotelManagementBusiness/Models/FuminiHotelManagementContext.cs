using Business.Models;
namespace Business.Models
{
    public class FuminiHotelManagementContext
    {
        public List<Customer> Customers { get; set; } = new();
        public List<RoomInformation> RoomInformations { get; set; } = new();
        public List<RoomType> RoomTypes { get; set; } = new();
        public List<BookingReservation> BookingReservations { get; set; } = new();
        public List<BookingDetail> BookingDetails { get; set; } = new();

        public FuminiHotelManagementContext()
        {
            // Add sample Room Types
            RoomTypes.Add(new RoomType { RoomTypeId = 1, RoomTypeName = "Standard Room", TypeDescription = "Basic room with amenities." });
            RoomTypes.Add(new RoomType { RoomTypeId = 2, RoomTypeName = "Deluxe Room", TypeDescription = "Upgraded features like a balcony." });

            // Add sample Rooms
            RoomInformations.Add(new RoomInformation { RoomId = 1, RoomNumber = "101", RoomTypeId = 1, RoomPricePerDay = 100 });
            RoomInformations.Add(new RoomInformation { RoomId = 2, RoomNumber = "202", RoomTypeId = 2, RoomPricePerDay = 150 });

            // Add sample Customers
            Customers.Add(new Customer { CustomerId = 1, CustomerFullName = "Alice Johnson", EmailAddress = "alice@example.com", CustomerStatus = 1 });
            Customers.Add(new Customer { CustomerId = 2, CustomerFullName = "Bob Smith", EmailAddress = "bob@example.com", CustomerStatus = 1 });

            // Add sample Booking Reservations
            BookingReservations.Add(new BookingReservation { BookingReservationId = 1, CustomerId = 1, BookingDate = DateTime.Today, BookingStatus = 1 });
            BookingReservations.Add(new BookingReservation { BookingReservationId = 2, CustomerId = 2, BookingDate = DateTime.Today, BookingStatus = 1 });

            // Add sample Booking Details
            BookingDetails.Add(new BookingDetail { BookingReservationId = 1, RoomId = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(3), ActualPrice = 300 });
            BookingDetails.Add(new BookingDetail { BookingReservationId = 2, RoomId = 2, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(2), ActualPrice = 300 });
        }

        public void SaveChanges()
        {
            // No need to implement for List<T>
        }
    }

}
