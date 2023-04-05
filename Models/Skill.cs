using System.ComponentModel.DataAnnotations;

namespace EMS.Models;

public class Skill
{
    [Key]
    public int skillId { get; set; }
    [Required]
    [Display(Name="Skill Name")]
    [StringLength(25)]
    public string name { get; set; }
    public ICollection<Employee>? employees{get; set;}
    public List<EmployeeSkill>? employeeSkills{ get; set; }

}
