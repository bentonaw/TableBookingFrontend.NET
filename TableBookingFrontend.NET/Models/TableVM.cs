namespace TableBookingFrontend.NET.Models
{
    public class TableVM
    {
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsCommunal { get; set; }
    }
}
