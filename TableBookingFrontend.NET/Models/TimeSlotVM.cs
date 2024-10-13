namespace TableBookingFrontend.NET.Models
{
    public class TimeSlotVM
    {
        public int TimeSlotId { get; set; }
        public int TimeSlotNr { get; set; }
        public bool LunchTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
