using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductionReportAPI.Models
{
    [Keyless]
    [Table("GroupSummaryRaw")] // Ini hanya anotasi, tidak harus ada di DB
    public class GroupSummaryRaw
    {
        public string Floor { get; set; }
        public string Line { get; set; }
        public string StationName { get; set; }
        public string GroupName { get; set; }
        public int PassCount { get; set; }
        public int FailCount { get; set; }
        public int Total { get; set; }
        public double PassRate { get; set; }
    }
}
