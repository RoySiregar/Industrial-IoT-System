using Microsoft.EntityFrameworkCore;
using ProductionReportAPI.Data;
using ProductionReportAPI.DTOs;
using ProductionReportAPI.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionReportAPI.Services;

public class CategoryDetailService
{
    private readonly ApplicationDbContext _context;

    public CategoryDetailService(ApplicationDbContext context)
    {
        _context = context;
    }

    // --- 1. GROUP SUMMARY ---
    public async Task<List<GroupSummaryDto>> GetGroupSummaryAsync(string line, DateTime start, DateTime end)
    {
        var result = await _context.KafkaLogDataTemp
            .AsNoTracking()
            .Where(k =>
                // [FOKUS EQUAL - AMAN]
                // MySQL otomatis ignore-case. Aman dari Error 500.
                k.Category.Equals(LogConstants.CategoryFixture) &&
                k.Line.Equals(line) &&
                k.CurrentTime >= start &&
                k.CurrentTime <= end)
            .GroupBy(k => new { k.Floor, k.Line, k.StationName, k.GroupName })
            .Select(g => new
            {
                g.Key.Floor,
                g.Key.Line,
                g.Key.StationName,
                g.Key.GroupName,

                // [FOKUS EQUAL]
                PassCount = g.Count(k => k.TestResult.Equals(LogConstants.ResultPass)),
                FailCount = g.Count(k => k.TestResult.Equals(LogConstants.ResultFail))
            })
            .ToListAsync();

        return result.Select(item =>
        {
            var total = item.PassCount + item.FailCount;
            var passRate = total == 0 ? 0 : Math.Round(item.PassCount * 100.0 / total, 2);

            return new GroupSummaryDto
            {
                Floor = item.Floor,
                Line = item.Line,
                StationName = item.StationName,
                GroupName = item.GroupName,
                PassCount = item.PassCount,
                FailCount = item.FailCount,
                Total = total,
                PassRate = passRate
            };
        })
        .OrderBy(r => r.Floor)
        .ThenBy(r => r.Line)
        .ThenBy(r => r.StationName)
        .ThenBy(r => r.GroupName)
        .ToList();
    }

    // --- 2. CATEGORY DETAILS ---
    public async Task<CategoryDetailResponseDto> GetCategoryDetailsAsync(
        string line, string stationName, string groupName, DateTime startDate, DateTime endDate)
    {
        var rawData = await _context.KafkaLogDataTemp
            .AsNoTracking()
            .Where(k =>
                // [FOKUS EQUAL - AMAN]
                k.Category.Equals(LogConstants.CategoryFixture) &&
                k.Line.Equals(line) &&
                k.StationName.Equals(stationName) &&
                k.GroupName.Equals(groupName) &&
                k.CurrentTime >= startDate &&
                k.CurrentTime <= endDate)
            .GroupBy(k => k.CategoryNumber)
            .Select(g => new
            {
                CategoryNumber = g.Key,
                // [FOKUS EQUAL]
                PassCount = g.Count(k => k.TestResult.Equals(LogConstants.ResultPass)),
                FailCount = g.Count(k => k.TestResult.Equals(LogConstants.ResultFail))
            })
            .ToListAsync();

        var totalPass = rawData.Sum(x => x.PassCount);
        var totalFail = rawData.Sum(x => x.FailCount);
        var totalAll = totalPass + totalFail;

        var summaryAll = new CategoryDetailDto
        {
            CategoryNumber = LogConstants.CategoryAll,
            PassCount = totalPass,
            FailCount = totalFail,
            Line = line,
            StationName = stationName,
            GroupName = groupName,
            Total = totalAll,
            PassRate = totalAll == 0 ? 0 : Math.Round(totalPass * 100.0 / totalAll, 2),
            UsageStatus = totalAll > 0 ? LogConstants.StatusUsed : LogConstants.StatusNotUsed
        };

        var detailList = rawData.Select(item =>
        {
            var total = item.PassCount + item.FailCount;
            return new CategoryDetailDto
            {
                Line = line,
                StationName = stationName,
                GroupName = groupName,
                CategoryNumber = item.CategoryNumber,
                PassCount = item.PassCount,
                FailCount = item.FailCount,
                Total = total,
                PassRate = total == 0 ? 0 : Math.Round(item.PassCount * 100.0 / total, 2),
                UsageStatus = total > 0 ? LogConstants.StatusUsed : LogConstants.StatusNotUsed
            };
        })
        .OrderBy(r => int.TryParse(r.CategoryNumber, out var num) ? num : int.MaxValue)
        .ToList();

        var finalData = new List<CategoryDetailDto> { summaryAll };
        finalData.AddRange(detailList);

        return new CategoryDetailResponseDto
        {
            TotalFixture = rawData.Count,
            UsedFixture = rawData.Count(d => (d.PassCount + d.FailCount) > 0),
            Data = finalData
        };
    }
}