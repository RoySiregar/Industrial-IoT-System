using System.Text.Json.Serialization;

namespace ProductionReportAPI.DTOs;

public class GroupSummaryDto
{
    public string? Line { get; set; }
    public string? Floor { get; set; }
    public string? StationName { get; set; }
    public string? GroupName { get; set; }
    [JsonPropertyName("passcount")]
    public int PassCount { get; set; }
    [JsonPropertyName("failcount")]
    public int FailCount { get; set; }
    public int Total { get; set; }
    [JsonPropertyName("passrate")]
    public double PassRate { get; set; }
}