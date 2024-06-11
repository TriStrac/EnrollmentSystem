using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystem.Web.Models.Database
{
    public class EnrollmentDetail
    {
        [Key]
        public int EDId { get; set; }
        public long StudentIdNumber { get; set; }
        public string SubjectCode { get; set; }
        public int EdpCode { get; set; }
        public string Status { get; set; }
    }
}
