using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }  // Primary Key
        public string RoomTypeName { get; set; } = string.Empty;
        public string? TypeDescription { get; set; }
        public string? TypeNote { get; set; }

        // Navigation Property
        public List<RoomInformation> RoomInformations { get; set; } = new();
    }
}
