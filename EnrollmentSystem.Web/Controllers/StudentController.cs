using EnrollmentSystem.Web.Data;
using EnrollmentSystem.Web.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnrollmentSystem.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly EnrollmentSystemDbContext _context;

        public StudentController(EnrollmentSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult StudentEntry()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitStudent(Student student)
        {
            if (await _context.StudentProperty.AnyAsync(s => s.IdNumber == student.IdNumber))
            {
                ModelState.AddModelError("IdNumber", "Duplicate id number");
            }

            if (ModelState.IsValid)
            {
                var sql = "SET IDENTITY_INSERT dbo.StudentProperty ON; " +
                          "INSERT INTO dbo.StudentProperty (IdNumber, LastName, FirstName, MiddleName, Course, Year, Remarks, Status) " +
                          "VALUES (@IdNumber, @LastName, @FirstName, @MiddleName, @Course, @Year, @Remarks, @Status); " +
                          "SET IDENTITY_INSERT dbo.StudentProperty OFF;";

                var parameters = new[]
                {
                    new SqlParameter("@IdNumber", student.IdNumber),
                    new SqlParameter("@LastName", student.LastName ?? (object)DBNull.Value),
                    new SqlParameter("@FirstName", student.FirstName ?? (object)DBNull.Value),
                    new SqlParameter("@MiddleName", student.MiddleName ?? (object)DBNull.Value),
                    new SqlParameter("@Course", student.Course ?? (object)DBNull.Value),
                    new SqlParameter("@Year", student.Year),
                    new SqlParameter("@Remarks", student.Remarks ?? (object)DBNull.Value),
                    new SqlParameter("@Status", student.Status ?? (object)DBNull.Value)
                };

                await _context.Database.ExecuteSqlRawAsync(sql, parameters);
                return RedirectToAction("StudentEntry");
            }
            return View("StudentEntry", student);
        }
    }
}
