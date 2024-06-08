using System.ComponentModel.DataAnnotations;

namespace EnrollmentSystem.Web.Models.Database
{
    public class EnrollmentHeader
    {
        [Key]
        public int EHId { get; set; }
        public long StudentIdNumber { get; set; }
        public string DateEnrolled { get; set; }
        public string Encoder {  get; set; }
        public int TotalUnits { get; set; }
        public string Status { get; set; }
    }
}
