using Data;
using Data.Exceptions;
using IServices;

namespace Services;

public class GuidedJournalLogService : IGuidedJournalLogService
{

    private readonly ApiContext _apiContext;

    public GuidedJournalLogService(
       ApiContext apiContext,
       IGuidedJournalEntryService guidedJournalEntryService
    )
    {
        _apiContext = apiContext;

    }

    public IEnumerable<Data.GuidedJournalLog> GetAllForUserId(Guid userId)
    {
        var userGuidedJournalLogs = _apiContext.GuidedJournalLogs.Where(gjl => gjl.UserId == userId);

        return userGuidedJournalLogs;
    }

    public GuidedJournalLog GetById(Guid id)
    {
        var existingLog = _apiContext.GuidedJournalLogs.SingleOrDefault(gjl => gjl.Id == id);

        if (existingLog == null)
        {
            throw new NotFoundException("Guided Journal Log");
        }

        return existingLog;
    }

    public void Add(GuidedJournalLog guidedJournalLogPassed)
    {

        var journalEntryForLog = _apiContext.GuidedJournalEntries.FirstOrDefault(gje => gje.Id == guidedJournalLogPassed.EntryId &&
                                                                                 gje.Deleted == false &&
                                                                                 gje.UserId == guidedJournalLogPassed.UserId);
        if (journalEntryForLog == null)
        {
            throw new NotFoundException("Journal Entry for Log");
        }

        var existsForSameDate = _apiContext.GuidedJournalLogs.FirstOrDefault(gjl => gjl.UserId == guidedJournalLogPassed.UserId &&
                                                                             gjl.Date == guidedJournalLogPassed.Date
                                                                             && gjl.EntryId == journalEntryForLog.Id);

        if (existsForSameDate != null)
        {
            throw new SameDateException("Guided Journal Log", guidedJournalLogPassed.Date.ToString() ?? "");
        }

        _apiContext.GuidedJournalLogs.Add(guidedJournalLogPassed);

        _apiContext.SaveChanges();
    }

    public void Update(GuidedJournalLog guidedJournalLogPassed, Guid id)
    {

        var existingJournalLogPassed = _apiContext.GuidedJournalLogs.FirstOrDefault(gjl => gjl.Id == id &&
                                                                                    gjl.UserId == guidedJournalLogPassed.UserId);

        if (existingJournalLogPassed == null)
        {
            throw new NotFoundException("Journal Log");
        }

            existingJournalLogPassed.EntryTextValue = guidedJournalLogPassed.EntryTextValue;

            existingJournalLogPassed.Date = guidedJournalLogPassed.Date;

            _apiContext.SaveChanges();
    }
}