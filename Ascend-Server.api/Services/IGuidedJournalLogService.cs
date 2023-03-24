using Models;

public interface IGuidedJournalLogService
{
    GuidedJournalLog[] GetAllForUserId(Guid userId);
    void Add(GuidedJournalLog guidedJournalLog);
    void Update(GuidedJournalLog guidedJournalLog, Guid id);
}