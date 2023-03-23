using EMS.Models;
using Microsoft.EntityFrameworkCore;
namespace EMS.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {   
    }

    public DbSet<Employee> Employees {get; set;}
    public DbSet<Skill> Skills {get; set;}

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasMany(e => e.skills)
            .WithMany(s => s.employees)
            .UsingEntity<EmployeeSkill>(
                j => j
                    .HasOne(es => es.skill)
                    .WithMany(s => s.employeeSkills)
                    .HasForeignKey(es => es.skillId),
                
                j => j
                    .HasOne(es => es.employee)
                    .WithMany(e => e.employeeSkills)
                    .HasForeignKey(es => es.employeeId),

                j =>
                {
                    j.Property(es => es.level);
                    j.Property(es =>es.experience);
                    // j.Property(es => es.skillType);
                    j.HasKey(t => new {t.employeeId, t.skillId});
                }
            );
    }

}