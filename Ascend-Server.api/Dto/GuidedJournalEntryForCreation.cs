namespace Ascend_Server.api.Dto;

public class GuidedJournalEntryForCreation
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? EntryName { get; set; }
    public bool? Deleted { get; set; }
}