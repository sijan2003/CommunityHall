using System.ComponentModel.DataAnnotations;

namespace EnhancedHallBooking.Models.ViewModels
{
    public class StatusCheckViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}