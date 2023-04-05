using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Models;
public class EmployeeSkill
{
    [Key]
    public string employeeSkillId {get; set;}
    [Required]
    public int employeeId { get; set; }
    public Employee? employee {get; set;}
    [Required]
    public int skillId { get; set; }
    public Skill? skill{get; set;}
    [Required]
    [Display(Name="Expert Level")]
    public string level { get; set; }
    [Required]
    [Display(Name="Experience")]
    public int experience { get; set; }
}
