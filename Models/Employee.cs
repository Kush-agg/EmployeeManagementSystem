using System.ComponentModel.DataAnnotations;
namespace EMS.Models;
public class Employee
{
    [Key]
    public int employeeId { get; set; }


    [Required]
    [StringLength(25)]
    [Display(Name ="First Name")]
    public string firstName{ get; set; }


    [Display(Name ="Last Name")]
    [StringLength(25)]
    public string lastName{ get; set; }


    [Required, DataType(DataType.Date)]
    [Display(Name ="Date of Joining")]
    public DateTime dateOfJoining{ get; set; }


    [Required]
    [StringLength(25)]
    [Display(Name ="Designation")]
    public string designation{ get; set; }
    

    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name="Email ID")]
    public string email{ get; set; }

    public ICollection<Skill>? skills{get; set;}
    public List<EmployeeSkill>? employeeSkills{ get; set; }
}