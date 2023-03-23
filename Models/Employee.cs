using System.ComponentModel.DataAnnotations;
namespace EMS.Models;
public class Employee
{
    [Key]
    public int employeeId { get; set; }
    [Required]
    public string firstName{ get; set; }
        
    public string lastName{ get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime dateOfJoining{ get; set; }

    [Required]
    public string designation{ get; set; }

    [Required]
    public string email{ get; set; }

    public ICollection<Skill>? skills{get; set;}
    public List<EmployeeSkill>? employeeSkills{ get; set; }
}