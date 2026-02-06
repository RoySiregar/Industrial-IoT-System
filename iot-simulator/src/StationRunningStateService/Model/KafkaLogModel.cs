using System;

namespace IndustrialIoTLogger
{
    public class KafkaLogData
    {
        // ID tidak perlu dimasukkan di sini karena AUTO_INCREMENT di DB
        public string? Factory { get; set; }
        public string? Floor { get; set; }
        public string? Line { get; set; }
        public string? Project { get; set; }
        public string? StationName { get; set; }
        public string? EquipmentName { get; set; }
        public DateTime CurrentTime { get; set; } // Ubah ke DateTime agar mudah diatur
        public string? Level { get; set; }
        public string? Category { get; set; }
        public string? CategoryNumber { get; set; }
        public string? ErrorCode { get; set; }
        public string? Description { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? TestResult { get; set; } // Bisa null
        public string? ISN { get; set; }
        public string? GroupName { get; set; }
        public string? FixtureResult { get; set; } // Bisa null
    }
}