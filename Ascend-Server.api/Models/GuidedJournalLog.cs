namespace Models; 

using System.ComponentModel.DataAnnotations;

public class GuidedJournalLog
{   
    [Key]
    public Guid Id {get; set;}
    [Required]
    public Guid UserId {get; set;}
    //front-end currently generates the UUID
    [Required]
    [RegularExpression(@"\b[A-Fa-f0-9]{8}(?:-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12}\b")]
    public Guid EntryId {get; set;}
    public string? EntryTextValue {get; set;}
    public DateOnly? Date {get; set;}

}