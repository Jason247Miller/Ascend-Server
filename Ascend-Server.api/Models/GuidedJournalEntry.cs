namespace Models; 
using System.ComponentModel.DataAnnotations;
public class GuidedJournalEntry
{   [Key]
    public int Id {get; set;}
    [Required]
    public int UserId {get; set;}
    [Required]
    public string? Uuid {get; set;}
    public string? EntryName {get; set;}
    public bool? Deleted {get; set;}
    
}