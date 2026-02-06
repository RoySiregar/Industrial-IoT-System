using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionReportAPI.Models;

[Table("fixture_test")]
public class FixtureTest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    // Tambahkan properti lain jika ada
}