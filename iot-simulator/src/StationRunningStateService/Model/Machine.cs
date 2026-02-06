namespace StationRunningStateService.Model
{
    public class Machine
    {
        public string Factory { get; set; } = "";
        public string Floor { get; set; } = "";
        public string Line { get; set; } = "";
        public string Code { get; set; } = "";
        public string Status { get; set; } = "IDLE";
        public string CurrentStep { get; set; } = "";
        public string StatusMsg { get; set; } = "";
        public int CntPass { get; set; } = 0;
        public int CntFail { get; set; } = 0;
        public double LastCT { get; set; } = 0;
        public DateTime LastUpdateTime { get; set; } = DateTime.Now;
    }
} 