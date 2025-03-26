using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class RoomInformation
    {
        public int RoomId { get; set; }  // Primary Key
        public string RoomNumber { get; set; } = string.Empty;
        public string? RoomDetailDescription { get; set; }
        public int? RoomMaxCapacity { get; set; }
        public int RoomTypeId { get; set; } // Foreign Key Reference
        public byte? RoomStatus { get; set; }
        public decimal? RoomPricePerDay { get; set; }

        // Navigation Property
        public RoomType? RoomType { get; set; }
        public List<BookingDetail> BookingDetails { get; set; } = new();
    }
}
