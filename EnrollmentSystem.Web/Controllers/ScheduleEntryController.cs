using System.Linq;
using EnrollmentSystem.Web.Data;
using EnrollmentSystem.Web.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnrollmentSystem.Web.Controllers
{
    public class ScheduleEntryController : Controller
    {
        private readonly EnrollmentSystemDbContext _context;

        public ScheduleEntryController(EnrollmentSystemDbContext context)
        {
            _context = context;
        }

        public IActionResult ScheduleEntry()
        {
            ViewBag.Subjects = _context.SubjectProperty.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitSched(SubjectSched subjectSched)
        {
            // Check if EdpCode exists
            if (await _context.SubjectSchedProperty.AnyAsync(s => s.EdpCode == subjectSched.EdpCode))
            {
                ModelState.AddModelError("EdpCode", "Edp Code already exists.");
            }

            if (ModelState.IsValid)
            {
                _context.SubjectSchedProperty.Add(subjectSched);
                await _context.SaveChangesAsync();
                return RedirectToAction("ScheduleEntry");
            }

            ViewBag.Subjects = _context.SubjectProperty.ToList(); // Reload subject list for dropdown
            return View("ScheduleEntry", subjectSched);
        }
    }
}
