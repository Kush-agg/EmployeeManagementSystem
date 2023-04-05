using System.ComponentModel.DataAnnotations;

namespace EMS.Models;

public class Login
{
    [Key]
    public int loginId {get; set;}
    [Required]
    [Display(Name="Username")]
    public string userName{get; set;}
    [Required]
    [Display(Name="Password")]
    [DataType(DataType.Password)]
    public string password{get; set;}

}