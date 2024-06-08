using Microsoft.AspNetCore.Mvc;

namespace EnrollmentSystem.Web.Controllers
{
    public class ScheduleEntryController : Controller
    {
        public IActionResult ScheduleEntry()
        {
            return View();
        }
    }
}
