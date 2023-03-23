namespace EMS.Models;
public class EmployeeSkill
{
    public int employeeId { get; set; }
    public Employee employee {get; set;}
    public int skillId { get; set; }
    public Skill skill{get; set;}
    public string level { get; set; }
    public int experience { get; set; }
}
