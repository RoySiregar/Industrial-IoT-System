using Microsoft.AspNetCore.Mvc;
using ProductionReportAPI.Services;
using System;
using System.Threading.Tasks;

namespace ProductionReportAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoricalDataController : ControllerBase
{
    private readonly HistoricalDataService _service;

    public HistoricalDataController(HistoricalDataService service)
    {
        _service = service;
    }

    [HttpGet("group-summary")]
    public async Task<IActionResult> GetGroupSummary([FromQuery] string line, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = await _service.GetGroupSummaryAsync(line, startDate, endDate);
        return Ok(result);
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetFailDetails([FromQuery] string line, [FromQuery] string stationName, [FromQuery] string groupName, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = await _service.GetFailDetailsAsync(line, stationName, groupName, startDate, endDate);
        return Ok(result);
    }

    [HttpGet("fail-type-chart")]
    public async Task<IActionResult> GetFailTypeChart([FromQuery] string line, [FromQuery] string stationName, [FromQuery] string groupName, [FromQuery] string categoryNumber, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = await _service.GetFailTypeChartAsync(line, stationName, groupName, categoryNumber, startDate, endDate);
        return Ok(result);
    }
}