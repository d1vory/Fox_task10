using System.ComponentModel.DataAnnotations;

namespace Task10.Models;

public class Group
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50)] 
    public string Name { get; set; }  = null!;
    
    public  int CourseId { get; set; }
    public Course Course { get; set; }  = null!;
    
}