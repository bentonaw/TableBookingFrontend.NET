using System.Reflection;

namespace TableBookingFrontend.NET.Models
{
    public class ReservationAndTimeSlotVM
    {
        public ReservationVM reservationVM { get; set; }
        public TimeSlotVM timeSlotVM { get; set; }
        public IEnumerable<TimeSlotVM> timeSlotVMs { get; set; }
    }
}
