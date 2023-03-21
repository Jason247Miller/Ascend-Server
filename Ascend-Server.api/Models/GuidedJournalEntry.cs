namespace Models; 

using System.ComponentModel.DataAnnotations;

public class GuidedJournalEntry
{
    [Key]
    public Guid Id {get; set;}
    [Required]
    public Guid UserId {get; set;}
    [Required]
    [RegularExpression(@"\b[A-Fa-f0-9]{8}(?:-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12}\b")]
    public string? EntryName {get; set;}
    public bool? Deleted {get; set;}
    
}