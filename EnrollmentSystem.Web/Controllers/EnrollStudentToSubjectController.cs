using EnrollmentSystem.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentSystem.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollStudentToSubjectController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly EnrollmentSystemDbContext _context;

        public EnrollStudentToSubjectController(EnrollmentSystemDbContext context, ILogger<StudentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult EnrollStudentToSubject()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(long id)
        {
            var student = _context.StudentProperty.FirstOrDefault(s => s.IdNumber == id);
            if (student == null)
            {
                _logger.LogWarning($"Student with ID {id} not found.");
                return NotFound();
            }
            _logger.LogInformation($"Student Found: {student.FirstName} {student.LastName}");
            return Ok(student);
        }

        [HttpGet("SubjectSchedule/{edpCode}")]
        public IActionResult GetSubjectSchedule(int edpCode)
        {
            var subject = _context.SubjectSchedProperty.FirstOrDefault(s => s.EdpCode == edpCode);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
    }
}
