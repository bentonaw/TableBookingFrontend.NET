using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TableBookingFrontend.NET.Models
{
    public class ReservationVM
    {
        public int ReservationId { get; set; }
        public CustomerVM Customer { get; set; }
        [Range(1, 8, ErrorMessage = "For parties more than 8 people, please contact through mail")]
        public int NrOfSeats { get; set; }
        [JsonIgnore]
        public DateTime ReservationDate { get; set; }
        public TimeSlotVM TimeSlot { get; set; }
        public TableVM Table { get; set; }
        public string FormattedReservationDate => ReservationDate.ToString("yyyy-MM-dd");
    }
}
