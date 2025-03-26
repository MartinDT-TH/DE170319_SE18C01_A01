using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class BookingReservation
    {
        public int BookingReservationId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public byte? BookingStatus { get; set; }

        // Navigation Properties
        public Customer? Customer { get; set; }
        public List<BookingDetail> BookingDetails { get; set; } = new();
    }
}
