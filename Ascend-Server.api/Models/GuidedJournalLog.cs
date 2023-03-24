namespace Models; 
using System.ComponentModel.DataAnnotations;
public class GuidedJournalLog
{   [Key]
    public int Id {get; set;}
    [Required]
    public int UserId {get; set;}
    [Required]
    public string? EntryId {get; set;}
    public string? EntryTextValue {get; set;}
    public DateOnly? Date {get; set;}
}