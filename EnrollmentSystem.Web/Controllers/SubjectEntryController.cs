using Microsoft.AspNetCore.Mvc;

namespace EnrollmentSystem.Web.Controllers
{
    public class SubjectEntryController : Controller
    {
        public IActionResult SubjectEntry()
        {
            return View();
        }
    }
}
