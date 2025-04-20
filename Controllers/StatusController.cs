using EnhancedHallBooking.Models;
using EnhancedHallBooking.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnhancedHallBooking.Controllers
{
    public class StatusController : Controller
    {
        private readonly BookingContext _context;

        public StatusController(BookingContext context)
        {
            _context = context;
        }

        public IActionResult Check() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Check(StatusCheckViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var bookings = await _context.Bookings
                .Where(b => b.Email == model.Email)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return View("Result", bookings);
        }
    }
}