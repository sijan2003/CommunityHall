using Microsoft.EntityFrameworkCore;

namespace EnhancedHallBooking.Models
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

        public DbSet<Booking> Bookings { get; set; }
    }
}