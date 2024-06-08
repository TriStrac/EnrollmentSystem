using EnrollmentSystem.Web.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentSystem.Web.Data
{
    public class EnrollmentSystemDbContext : DbContext
    {
        public EnrollmentSystemDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> StudentProperty { get; set; }
        public DbSet<Subject> SubjectProperty { get; set; }
        public DbSet<SubjectSched> SubjectSchedProperty { get; set; }
        public DbSet<EnrollmentHeader> EnrollmentHeaderProperty { get; set; }
        public DbSet<EnrollmentDetail> EnrollmentDetailProperty { get; set; }
    }
}
