using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductionReportAPI.DTOs;

public class FixtureCategorySummaryDto
{
    public string? Line { get; set; }
    public string? StationName { get; set; }
    public string? GroupName { get; set; }

    [Column("passcount")] // Cocokkan dengan nama kolom dari query
    public int PassCount { get; set; }

    [Column("failcount")] // Cocokkan dengan nama kolom dari query
    public int FailCount { get; set; }

    public int Total { get; set; }

    [Column("PassRate(%)")] // Cocokkan dengan nama kolom dari query
    [JsonPropertyName("PassRate(%)")]
    public double PassRatePercentage { get; set; }
}