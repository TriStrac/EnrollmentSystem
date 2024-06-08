using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystem.Web.Models.Database
{
    public class Subject
    {
        [Key]
        public string SubjectCode { get; set; }
        public string SubjectDesc { get; set; }
        public int Units { get; set; }
        public int RegOffer { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string CourseCode { get; set; }
        public string Curriculum {  get; set; }
    }
}
