using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionReportAPI.Models;

[Table("kafka_log_data_temp")]
public class KafkaLogDataTemp
{
    public long Id { get; set; }
    public string? Factory { get; set; }
    public string? Floor { get; set; }
    public string? Line { get; set; }
    public string? Project { get; set; }
    public string? StationName { get; set; }
    public string? EquipmentName { get; set; }
    public DateTime? CurrentTime { get; set; }
    public string? Level { get; set; }
    public string? Category { get; set; }
    public string? CategoryNumber { get; set; }
    public string? ErrorCode { get; set; }
    public string? Description { get; set; }
    public string? Message { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? TestResult { get; set; }
    public string? FixtureResult { get; set; } // Pastikan properti ini ada
    public string? ISN { get; set; }
    public string? GroupName { get; set; }
}