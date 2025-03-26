using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? CustomerFullName { get; set; }
        public string? Telephone { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime? CustomerBirthday { get; set; }
        public byte? CustomerStatus { get; set; }
        public string? Password { get; set; }

        // Navigation Property
        public List<BookingReservation> BookingReservations { get; set; } = new();
    }
}
