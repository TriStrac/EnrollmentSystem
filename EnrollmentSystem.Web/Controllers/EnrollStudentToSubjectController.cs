using EnrollmentSystem.Web.Data;
using EnrollmentSystem.Web.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

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

        [HttpGet("{id}/EnrolledSubjects")]
        public IActionResult GetEnrolledSubjects(long id)
        {
            try
            {
                // Retrieve the enrolled subjects for the student with the specified ID
                var enrolledSubjects = _context.EnrollmentDetailProperty
                    .Where(e => e.StudentIdNumber == id && e.Status == "Active") // Filter by student ID and active status
                    .Select(e => new { e.EdpCode }) // Select only the EdpCode
                    .ToList();

                if (enrolledSubjects == null || enrolledSubjects.Count == 0)
                {
                    return NotFound("No enrolled subjects found for the student.");
                }

                return Ok(enrolledSubjects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving enrolled subjects.");
                return StatusCode(500, "Error occurred while retrieving enrolled subjects. Please try again later.");
            }
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

        [HttpPost("EnrollHeader")]
        public IActionResult EnrollStudent([FromBody] EnrollmentHeader enrollmentHeader)
        {
            try
            {

                // Get the necessary information for the enrollment header
                var studentIdNumber = enrollmentHeader.StudentIdNumber;
                var encoder = enrollmentHeader.Encoder;
                var totalUnits = enrollmentHeader.TotalUnits;
                var dateEnrolled = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Convert to string


                // Create a new EnrollmentHeader object with this information
                var newEnrollmentHeader = new EnrollmentHeader
                {
                    StudentIdNumber = studentIdNumber,
                    DateEnrolled = dateEnrolled,
                    Encoder = encoder,
                    TotalUnits = totalUnits,
                    Status = "Active"
                };

                // Save the new enrollment header to the database
                _context.EnrollmentHeaderProperty.Add(newEnrollmentHeader);
                _context.SaveChanges();

                return Ok(newEnrollmentHeader);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while enrolling student.");
                return StatusCode(500, "Error occurred while enrolling student.");
            }
        }

        [HttpPost("EnrollDetails")]
        public IActionResult EnrollStudent([FromBody] List<EnrollmentDetail> enrollmentDetails)
        {
            try
            {
                if (enrollmentDetails == null || enrollmentDetails.Count == 0)
                {
                    return BadRequest("No enrollment details provided.");
                }

                // Assuming all enrollment details are for the same student
                var studentIdNumber = enrollmentDetails.First().StudentIdNumber;

                // Retrieve the current enrolled subjects from the database for the student
                var currentEnrolledSubjects = _context.EnrollmentDetailProperty
                    .Where(e => e.StudentIdNumber == studentIdNumber && e.Status == "Active")
                    .ToList();

                // List of EDP codes from the request
                var enrolledEdpCodes = enrollmentDetails.Select(e => e.EdpCode).ToList();

                // Find subjects to delete (exist in the database but not in the new enrolled list)
                var subjectsToDelete = currentEnrolledSubjects
                    .Where(e => !enrolledEdpCodes.Contains(e.EdpCode))
                    .ToList();

                // Delete subjects from the database
                _context.EnrollmentDetailProperty.RemoveRange(subjectsToDelete);

                // Find subjects to add (exist in the new enrolled list but not in the database)
                var subjectsToAdd = enrollmentDetails
                    .Where(e => !currentEnrolledSubjects.Any(c => c.EdpCode == e.EdpCode))
                    .ToList();

                // Add new subjects to the database
                foreach (var detail in subjectsToAdd)
                {
                    // Get SubjectCode from SubjectProperty if it's not provided
                    if (string.IsNullOrEmpty(detail.SubjectCode))
                    {
                        detail.SubjectCode = _context.SubjectSchedProperty
                            .Where(s => s.EdpCode == detail.EdpCode)
                            .Select(s => s.SubjectCode)
                            .FirstOrDefault();
                    }

                    detail.Status = "Active";
                    _context.EnrollmentDetailProperty.Add(detail);
                }

                // Save changes to the database
                _context.SaveChanges();

                return Ok("Enrollment successfully updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while enrolling student.");
                return StatusCode(500, "Error occurred while enrolling student.");
            }
        }


    }
}