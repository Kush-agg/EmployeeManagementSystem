using System.ComponentModel.DataAnnotations;
namespace EMS.Models;

public class ExpertLevel
{
    [Key]
    public int Id{get; set;}

    [Required]
    [StringLength(25)]
    public string name{get; set;}
}