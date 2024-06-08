using Microsoft.AspNetCore.Mvc;

namespace EnrollmentSystem.Web.Controllers
{
    public class StudentController : Controller
    {
   
        public IActionResult StudentEntry()
        {
            return View();
        }
        
    }
}
