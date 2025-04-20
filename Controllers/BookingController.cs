using EnhancedHallBooking.Models;
using EnhancedHallBooking.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EnhancedHallBooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingContext _context;
        private readonly ILogger<BookingController> _logger;

        public BookingController(BookingContext context, ILogger<BookingController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Create() => View();

        public IActionResult Calendar() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Validate date (no past dates)
            if (model.BookingDate.Date < DateTime.Today)
            {
                ModelState.AddModelError("BookingDate", "Booking date cannot be in the past.");
                return View(model);
            }

            // Validate time (end time after start time)
            if (model.StartTime >= model.EndTime)
            {
                ModelState.AddModelError("EndTime", "End time must be after start time.");
                return View(model);
            }

            // Check for double-booking (client-side evaluation)
            try
            {
                var bookingDate = model.BookingDate.Date;
                var bookings = await _context.Bookings
                    .Where(b => b.Facility == model.Facility)
                    .ToListAsync();

                var conflictingBooking = bookings.Any(b =>
                    b.BookingDate.Date == bookingDate &&
                    b.StartTime <= model.EndTime &&
                    b.EndTime >= model.StartTime);

                if (conflictingBooking)
                {
                    ModelState.AddModelError("", "This time slot is already booked.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing double-booking check.");
                ModelState.AddModelError("", "An error occurred while checking availability. Please try again.");
                return View(model);
            }

            // Save booking
            var booking = new Booking
            {
                Name = model.Name,
                Email = model.Email,
                Facility = model.Facility,
                BookingDate = model.BookingDate,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Status = "Pending"
            };

            _context.Add(booking);
            await _context.SaveChangesAsync();

            // Simulate email confirmation
            Console.WriteLine($"Booking Confirmation: {booking.Name}, {booking.Email}, {booking.Facility}, {booking.BookingDate:yyyy-MM-dd}, {booking.StartTime}-{booking.EndTime}");

            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation() => View();

        public IActionResult Admin() => View(_context.Bookings.OrderByDescending(b => b.CreatedAt).ToList());

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            if (!new[] { "Pending", "Approved", "Rejected" }.Contains(status))
            {
                return BadRequest("Invalid status.");
            }

            booking.Status = status;
            await _context.SaveChangesAsync();

            // Simulate email notification
            Console.WriteLine($"Status Updated: {booking.Email}, Booking ID: {id}, New Status: {status}");

            return RedirectToAction("Admin");
        }

        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _context.Bookings.Select(b => new
            {
                title = $"{b.Facility} ({b.Status})",
                start = b.BookingDate.ToString("yyyy-MM-dd") + "T" + b.StartTime.ToString(@"hh\:mm"),
                end = b.BookingDate.ToString("yyyy-MM-dd") + "T" + b.EndTime.ToString(@"hh\:mm"),
                color = b.Status == "Approved" ? "#28a745" : b.Status == "Pending" ? "#ffc107" : "#dc3545"
            }).ToListAsync();

            return Json(bookings);
        }
    }
}