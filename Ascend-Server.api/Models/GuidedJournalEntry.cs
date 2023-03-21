namespace Models; 

using System.ComponentModel.DataAnnotations;

public class GuidedJournalEntry
{
    [Key]
    public int Id {get; set;}
    [Required]
    public int UserId {get; set;}
    //front-end currently generates the UUID
    [Required]
    [RegularExpression(@"\b[A-Fa-f0-9]{8}(?:-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12}\b")]
    public string? Uuid {get; set;}
    public string? EntryName {get; set;}
    public bool? Deleted {get; set;}
    
}