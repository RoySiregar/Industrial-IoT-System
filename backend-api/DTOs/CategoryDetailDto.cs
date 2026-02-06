namespace ProductionReportAPI.DTOs;

public class CategoryDetailDto
{
    public string Line { get; set; }
    public string StationName { get; set; }
    public string GroupName { get; set; }
    public string CategoryNumber { get; set; }

    // Backend Clean HARUS PascalCase
    public int PassCount { get; set; }
    public int FailCount { get; set; }
    public int Total { get; set; }
    public double PassRate { get; set; }
    public string UsageStatus { get; set; }
}