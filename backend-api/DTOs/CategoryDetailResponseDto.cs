using System.Collections.Generic;

namespace ProductionReportAPI.DTOs;

public class CategoryDetailResponseDto
{
    public int TotalFixture { get; set; }
    public int UsedFixture { get; set; }
    public List<CategoryDetailDto> Data { get; set; } = new List<CategoryDetailDto>();
}