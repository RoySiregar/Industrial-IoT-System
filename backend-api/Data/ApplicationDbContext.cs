using Microsoft.EntityFrameworkCore;
using ProductionReportAPI.DTOs;
using ProductionReportAPI.Models;

namespace ProductionReportAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Model utama
    public DbSet<KafkaLogDataTemp> KafkaLogDataTemp { get; set; } = null!;
    public DbSet<StationTest> StationTests { get; set; } = null!;
    public DbSet<FixtureTest> FixtureTests { get; set; } = null!;



    // DTOs keyless untuk raw SQL
    public DbSet<FixtureCategorySummaryDto> FixtureSummaries { get; set; } = null!;
    public DbSet<LineStatusDto> LineStatuses { get; set; } = null!;
    public DbSet<LastErrorStatusDto> LastErrorStatus { get; set; } = null!;

    // Raw SQL result mapping untuk GroupSummary
    public DbSet<GroupSummaryRaw> GroupSummaryRaw { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // DTO hasil query raw SQL
        modelBuilder.Entity<FixtureCategorySummaryDto>().HasNoKey().ToView(null);
        modelBuilder.Entity<LineStatusDto>().HasNoKey().ToView(null);
        modelBuilder.Entity<LastErrorStatusDto>().HasNoKey().ToView(null);

        // Entity khusus untuk raw SQL query summary
        modelBuilder.Entity<GroupSummaryRaw>().HasNoKey(); // Tidak perlu .ToView(null) kalau pakai FromSqlRaw
    }
}
