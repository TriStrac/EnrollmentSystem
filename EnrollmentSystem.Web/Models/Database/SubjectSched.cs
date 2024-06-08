using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystem.Web.Models.Database
{
    public class SubjectSched
    {
        [Key]
        public int EdpCode { get; set; }
        public string SubjectCode { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Days { get; set; }
        public string Room { get; set; }
        public int MaxSize { get; set; }
        public int ClassSize { get; set; }
        public string Status { get; set; }
        public string XM {  get; set; }
        public string Section { get; set; }
        public int SchoolYear { get; set; }
    }
}
