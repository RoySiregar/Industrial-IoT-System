using System.ComponentModel.DataAnnotations.Schema;
namespace ProductionReportAPI.Models;

[Table("station_test")]
public class StationTest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    [Column("project_id")]
    public int ProjectId { get; set; }
}