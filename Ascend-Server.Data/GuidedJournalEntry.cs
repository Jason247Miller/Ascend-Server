namespace Data; 

public class GuidedJournalEntry
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? EntryName { get; set; }
    public bool? Deleted { get; set; }
    public DateOnly CreationDate { get; set; }
}