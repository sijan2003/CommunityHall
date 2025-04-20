using System.ComponentModel.DataAnnotations;

namespace EnhancedHallBooking.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string Facility { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Required, DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Required, DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        [Required]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}