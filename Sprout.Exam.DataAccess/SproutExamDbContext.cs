using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Models;

namespace Sprout.Exam.DataAccess
{
    public class SproutExamDbContext : DbContext
    {

        public SproutExamDbContext(DbContextOptions<SproutExamDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }

    }
}
