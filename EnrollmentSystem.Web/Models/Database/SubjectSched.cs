using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrollmentSystem.Web.Models.Database
{
    public class SubjectSched
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EdpCode { get; set; }

        [Required(ErrorMessage = "Subject Code is required")]
        public string SubjectCode { get; set; }

        [Required(ErrorMessage = "Start Time is required")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "End Time is required")]
        public string EndTime { get; set; }

        [Required(ErrorMessage = "Days is required")]
        public string Days { get; set; }

        [Required(ErrorMessage = "Room is required")]
        public string Room { get; set; }

        [Required(ErrorMessage = "Max Size is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Max Size must be greater than 0")]
        public int MaxSize { get; set; }

        public int ClassSize { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "XM is required")]
        public string XM { get; set; }

        [Required(ErrorMessage = "Section is required")]
        public string Section { get; set; }

        [Required(ErrorMessage = "School Year is required")]
        public int SchoolYear { get; set; }
    }
}
