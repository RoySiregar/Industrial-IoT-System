using System;

namespace ProductionReportAPI.DTOs;

public class LogMessageDto
{
    public DateTime? CurrentTime { get; set; }
    public string? Message { get; set; }
}