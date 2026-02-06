using Microsoft.AspNetCore.Mvc;
using ProductionReportAPI.Services;
using System;
using System.Threading.Tasks;

namespace ProductionReportAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryDetailController : ControllerBase
{
    private readonly CategoryDetailService _service;

    public CategoryDetailController(CategoryDetailService service)
    {
        _service = service;
    }

    [HttpGet("group-summary")]
    public async Task<IActionResult> GetGroupSummary([FromQuery] string line, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (string.IsNullOrEmpty(line)) return BadRequest("Line cannot be empty.");
        if (startDate >= endDate) return BadRequest("Start date must be earlier than end date.");
        var result = await _service.GetGroupSummaryAsync(line, startDate, endDate);
        return Ok(result);
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetCategoryDetails([FromQuery] string line, [FromQuery] string stationName, [FromQuery] string groupName, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(line) || string.IsNullOrWhiteSpace(stationName) || string.IsNullOrWhiteSpace(groupName)) return BadRequest("Invalid Input");
        if (startDate >= endDate) return BadRequest("Start date must be earlier than end date.");
        var result = await _service.GetCategoryDetailsAsync(line, stationName, groupName, startDate, endDate);
        return Ok(result);
    }
}