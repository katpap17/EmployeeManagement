using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Models
{
    public class SkillContext : DbContext
    {
        public SkillContext(DbContextOptions<SkillContext> options)
    : base(options)
        {
        }

        public DbSet<Skill> Skill { get; set; }
    }
}
