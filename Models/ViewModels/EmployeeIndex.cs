namespace EMS.Models.ViewModels;

public class EmployeeIndex
{
    public List<Employee> Employees{get; set;}
    public List<Skill> Skills{get; set;}
    public List<EmployeeSkill> employeeSkills{get; set;}

    public EmployeeSkill employeeSkill {get; set;}
}