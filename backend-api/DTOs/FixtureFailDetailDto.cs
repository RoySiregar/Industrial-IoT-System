namespace ProductionReportAPI.DTOs;

public class FixtureFailDetailDto
{
    public string? GroupName { get; set; }
    public string? FixtureName { get; set; }
    public int FailCount { get; set; }
}