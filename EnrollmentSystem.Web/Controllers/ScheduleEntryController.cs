using EnrollmentSystem.Web.Data;
using EnrollmentSystem.Web.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT SubjectSchedProperty ON");

                        _context.SubjectSchedProperty.Add(subjectSched);
                        await _context.SaveChangesAsync();

                        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT SubjectSchedProperty OFF");

                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        ModelState.AddModelError("", "An error occurred while saving the schedule.");
                    }
                }

                return RedirectToAction("ScheduleEntry");
            }

            ViewBag.Subjects = _context.SubjectProperty.ToList(); // Reload subject list for dropdown
            return View("ScheduleEntry", subjectSched);
        }
    }
}
