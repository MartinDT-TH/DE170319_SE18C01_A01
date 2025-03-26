using System;

namespace Business.Models
{
    public class BookingDetail
    {
        public int BookingReservationId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? ActualPrice { get; set; }

        // Navigation Properties
        public BookingReservation? BookingReservation { get; set; }
        public RoomInformation? Room { get; set; }
    }
}
