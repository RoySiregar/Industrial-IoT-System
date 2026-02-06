using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionReportAPI.DTOs;

public class LineStatusDto
{
    [Column("total_line")]
    public int TotalLine { get; set; }

    [Column("aktif_line")]
    public int AktifLine { get; set; }

    [Column("tidak_aktif_line")]
    public int TidakAktifLine { get; set; }
}