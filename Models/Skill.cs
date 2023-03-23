using System.ComponentModel.DataAnnotations;

namespace EMS.Models;

public class Skill
{
    [Key]
    public int skillId { get; set; }
    [Required]
    public string name { get; set; }
    public ICollection<Employee>? employees{get; set;}
    public List<EmployeeSkill>? employeeSkills{ get; set; }

}
