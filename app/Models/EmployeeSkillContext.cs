using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Models
{
    public class EmployeeSkillContext : DbContext
    {
        public EmployeeSkillContext(DbContextOptions<EmployeeSkillContext> options)
   : base(options)
        {
        }

        public DbSet<Employee> employee { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<EmployeeSkill> employeeSkill { get; set; }
    }
}
