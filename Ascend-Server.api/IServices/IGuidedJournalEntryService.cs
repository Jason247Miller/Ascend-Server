using Models;

public interface IGuidedJournalEntryService
{
    GuidedJournalEntry[] GetAllForUserId(Guid userId);
    void Add(GuidedJournalEntry guidedJournalEntry);
    void Update(GuidedJournalEntry guidedJournalEntry, Guid id);
}