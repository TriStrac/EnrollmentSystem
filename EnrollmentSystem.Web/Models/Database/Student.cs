using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystem.Web.Models.Database
{
    public class Student
    {
        [Key]
        public long IdNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Course { get; set; }
        public int  Year { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
    }
}
