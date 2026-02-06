using Microsoft.EntityFrameworkCore;
using ProductionReportAPI.Data;
using ProductionReportAPI.DTOs;
using ProductionReportAPI.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionReportAPI.Services;

public class HistoricalDataService
{
    private readonly ApplicationDbContext _context;

    public HistoricalDataService(ApplicationDbContext context)
    {
        _context = context;
    }

    // --- 1. GROUP SUMMARY ---
    public async Task<List<GroupSummaryDto>> GetGroupSummaryAsync(string line, DateTime start, DateTime end)
    {
        var result = await _context.KafkaLogDataTemp
            .AsNoTracking()
            .Where(k => 
                // [FOKUS EQUAL]
                // Menggunakan .Equals() sesuai saran atasan.
                // MySQL otomatis mengabaikan huruf besar/kecil (IgnoreCase) di sini.
                k.Category.Equals(LogConstants.CategoryFixture) &&
                k.Line.Equals(line) && // Konsisten pakai Equals semua
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
                PassCount = g.Count(k => k.FixtureResult.Equals(LogConstants.ResultPass)),
                FailCount = g.Count(k => k.FixtureResult.Equals(LogConstants.ResultFail))
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

    // --- 2. FAIL DETAILS ---
    public async Task<List<FixtureFailDetailDto>> GetFailDetailsAsync(string line, string station, string group, DateTime start, DateTime end)
    {
        var result = await _context.KafkaLogDataTemp
            .AsNoTracking()
            .Where(k => 
                // [FOKUS EQUAL]
                k.Category.Equals(LogConstants.CategoryFixture) &&
                k.FixtureResult.Equals(LogConstants.ResultFail) &&
                k.Line.Equals(line) &&
                k.StationName.Equals(station) &&
                k.GroupName.Equals(group) &&
                k.CurrentTime >= start &&
                k.CurrentTime <= end)
            .GroupBy(k => new { k.GroupName, k.CategoryNumber })
            .Select(g => new FixtureFailDetailDto
            {
                GroupName = g.Key.GroupName,
                FixtureName = "Fixture" + g.Key.CategoryNumber,
                FailCount = g.Count()
            })
            .Take(500)
            .ToListAsync();

        return result
            // Sorting di memori tetap pakai string biasa karena nama fixture bervariasi
            .OrderBy(dto => int.TryParse(dto.FixtureName.Replace("Fixture", ""), out int n) ? n : 0)
            .ToList();
    }

    // --- 3. LOG MESSAGES ---
    public async Task<List<LogMessageDto>> GetLogMessagesAsync(string line, string station, string group, string catNum, DateTime start, DateTime end)
    {
        return await _context.KafkaLogDataTemp
            .AsNoTracking()
            .Where(k => 
                // [FOKUS EQUAL]
                k.Category.Equals(LogConstants.CategoryFixture) &&
                k.FixtureResult.Equals(LogConstants.ResultFail) &&
                k.Line.Equals(line) &&
                k.StationName.Equals(station) &&
                k.GroupName.Equals(group) &&
                k.CategoryNumber.Equals(catNum) &&
                k.CurrentTime >= start &&
                k.CurrentTime <= end)
            .OrderByDescending(k => k.CurrentTime)
            .Take(100)
            .Select(k => new LogMessageDto
            {
                CurrentTime = k.CurrentTime,
                Message = k.Message
            })
            .ToListAsync();
    }

    // --- 4. FAIL TYPE CHART ---
    public async Task<List<FailTypeChartDto>> GetFailTypeChartAsync(string line, string station, string group, string catNum, DateTime start, DateTime end)
    {
        var queryData = await _context.KafkaLogDataTemp
            .AsNoTracking()
            .Where(k => 
                // [FOKUS EQUAL]
                k.Category.Equals(LogConstants.CategoryFixture) &&
                k.FixtureResult.Equals(LogConstants.ResultFail) &&
                k.Line.Equals(line) &&
                k.StationName.Equals(station) &&
                k.GroupName.Equals(group) &&
                k.CategoryNumber.Equals(catNum) &&
                k.CurrentTime >= start &&
                k.CurrentTime <= end)
            .Select(k => new { k.CategoryNumber, k.Message }) 
            .ToListAsync(); 

        var groupedResult = queryData
            .Select(k => new
            {
                k.CategoryNumber,
                // Helper logic string parsing tetap di sini
                FailType = GetFailTypeFromMessage(k.Message)
            })
            .GroupBy(x => new { x.CategoryNumber, x.FailType })
            .Select(g => new FailTypeChartDto
            {
                FixtureName = "Fixture" + g.Key.CategoryNumber,
                FailType = g.Key.FailType,
                FailCount = g.Count()
            })
            .OrderBy(dto => dto.FixtureName)
            .ThenByDescending(dto => dto.FailCount)
            .ToList();

        return groupedResult;
    }

    // Helper Private (Tetap gunakan IgnoreCase karena ini berjalan di MEMORY C#, bukan SQL)
    // Di sini code Atasan Anda (StringComparison.OrdinalIgnoreCase) BISA dan WAJIB dipakai.
    private string GetFailTypeFromMessage(string message)
    {
        if (string.IsNullOrEmpty(message)) return "Other";

        // [FOKUS EQUAL & COMPLIANCE]
        // Disini kita bisa pakai IndexOf dengan StringComparison karena sudah di Memory
        bool Contains(string keyword) => message.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;

        if (Contains("close test device")) return "CloseFail";
        if (Contains("open test device")) return "OpenFail";
        if (Contains("test timeout")) return "Timeout";
        if (Contains("check test device status")) return "CheckStatusFail";
        if (Contains("continuous fail")) return "ContinuousFail";
        
        return "Other";
    }
}