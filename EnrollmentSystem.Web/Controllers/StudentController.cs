using Microsoft.AspNetCore.Mvc;

namespace EnrollmentSystem.Web.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult StudentEntry()
        {
            return View();
        }

        public IActionResult SubjectEntry()
        {
            return View();
        }

        public IActionResult ScheduleEntry()
        {
            return View();
        }
        
        public IActionResult EnrollStudentToSubject()
        {
            return View();
        }
    }
}
