namespace StationRunningStateService.Model
{
    public class MQTTMessage
    {
        public string Factory { get; set; } = "";
        public string Floor { get; set; } = "";
        public string Line { get; set; } = "";
        public string Project { get; set; } = "";
        public string StationName { get; set; } = "";
        public string EquipmentName { get; set; } = "";
        public string CurrentTime { get; set; } = "";
        public string Level { get; set; } = "";
        public string Category { get; set; } = "";
        public string CategoryNumber { get; set; } = "";
        public string ErrorCode { get; set; } = "";
        public string Desc { get; set; } = "";
        public string Message { get; set; } = "";
    }
}