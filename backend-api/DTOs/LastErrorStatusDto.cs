namespace ProductionReportAPI.DTOs
{
    public class LastErrorStatusDto
    {
        public string Line { get; set; } = null!;
        public string Station { get; set; } = null!;
        public string LastTime { get; set; } = null!;
        public string LastLevel { get; set; } = null!;
        public string LastError { get; set; } = null!;
        public string StatusColor { get; set; } = null!;
    }
}
