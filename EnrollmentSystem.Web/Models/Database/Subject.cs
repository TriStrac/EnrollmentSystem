using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystem.Web.Models.Database
{
    public class Subject
    {
        [Key]
        [Required]
        public string SubjectCode { get; set; }

        [Required]
        public string SubjectDesc { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Units must be a positive number")]
        public int Units { get; set; }

        [Required]
        public int RegOffer { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string CourseCode { get; set; }

        [Required]
        public string Curriculum { get; set; }
    }
}
