namespace Models; 

using System.ComponentModel.DataAnnotations;

public class GuidedJournalLog
{   
    [Key]
    public int Id {get; set;}
    [Required]
    public int UserId {get; set;}
    //front-end currently generates the UUID
    [Required]
    [RegularExpression(@"\b[A-Fa-f0-9]{8}(?:-[A-Fa-f0-9]{4}){3}-[A-Fa-f0-9]{12}\b")]
    public string? EntryId {get; set;}
    public string? EntryTextValue {get; set;}
    public DateOnly? Date {get; set;}

}