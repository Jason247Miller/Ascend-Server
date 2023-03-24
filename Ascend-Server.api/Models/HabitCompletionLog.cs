namespace Models; 
using System.ComponentModel.DataAnnotations;
public class HabitCompletionLog
{   [Key]
    public int Id {get; set;}
    [Required]
    public int UserId {get; set;}
    [Required]
    public string? HabitId {get; set;}
    public bool? Completed {get; set;}
    public DateOnly? Date {get; set;}
}