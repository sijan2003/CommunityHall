using System.ComponentModel.DataAnnotations;

namespace EnhancedHallBooking.Models.ViewModels
{
    public class BookingViewModel
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Facility { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Required, DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Required, DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }
    }
}