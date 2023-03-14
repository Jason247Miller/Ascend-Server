namespace Models; 
using System.ComponentModel.DataAnnotations;
public class Habit
{   
    [Required]
    public int Id {get; set;}
    [Required]
    public int UserId {get; set;}
    [Required]
    public string? Uuid {get; set;}
    public bool? Deleted {get; set;}
    public string? HabitName {get; set;}
}