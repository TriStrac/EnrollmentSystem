using EnrollmentSystem.Web.Data;
using EnrollmentSystem.Web.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnrollmentSystem.Web.Controllers
{
    public class SubjectEntryController : Controller
    {
        private readonly EnrollmentSystemDbContext _context;

        public SubjectEntryController(EnrollmentSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult SubjectEntry()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitSubject(Subject subject)
        {
            // Check for duplicate SubjectCode
            if (await _context.SubjectProperty.AnyAsync(s => s.SubjectCode == subject.SubjectCode))
            {
                ModelState.AddModelError("SubjectCode", "Subject code already exists.");
            }

            if (ModelState.IsValid)
            {
                _context.SubjectProperty.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction("SubjectEntry");
            }

            return View("SubjectEntry", subject);
        }
    }
}
