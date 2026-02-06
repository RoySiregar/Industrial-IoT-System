namespace ProductionReportAPI.DTOs;

public class FailTypeChartDto
{
    public string? FixtureName { get; set; }
    public string? Message { get; set; }
    public string? FailType { get; set; }
    public int FailCount { get; set; }
}