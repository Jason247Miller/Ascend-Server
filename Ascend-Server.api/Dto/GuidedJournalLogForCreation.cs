namespace Ascend_Server.api.Dto;

public class GuidedJournalLogForCreation
{
    public string? Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EntryId { get; set; }
    public string? EntryTextValue { get; set; }
    public DateOnly? Date { get; set; }
}