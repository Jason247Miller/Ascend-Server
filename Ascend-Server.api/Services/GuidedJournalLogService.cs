using Models;
using Exceptions;
using System;
using Services;
namespace Services;

public class GuidedJournalLogService : IGuidedJournalLogService
{

    private readonly IUserService _userService;

    private readonly ApiContext _apiContext;

    public GuidedJournalLogService(
       ApiContext apiContext,
       IUserService userService,
       IGuidedJournalEntryService guidedJournalEntryService
    )
    {
        _apiContext = apiContext;

        _userService = userService;
    }

    public GuidedJournalLog[] GetAllForUserId(Guid userId)
    {
        _userService.CheckUserId(userId); 
        
        var userGuidedJournalLogs = _apiContext.GuidedJournalLogs.Where(gjl => gjl.UserId == userId).ToArray();

        return userGuidedJournalLogs;
    }

    public void Add(GuidedJournalLog guidedJournalLogPassed)
    {
        _userService.CheckUserId(guidedJournalLogPassed.UserId);

        var journalEntryForLog = _apiContext.GuidedJournalEntries.FirstOrDefault(gje => gje.Id == guidedJournalLogPassed.EntryId &&
                                                                                 gje.Deleted == false &&
                                                                                 gje.UserId == guidedJournalLogPassed.UserId);

        if (journalEntryForLog == null)
        {
            throw new NotFoundException("Journal Entry for Journal Log");
        }

        var existsForSameDate = _apiContext.GuidedJournalLogs.Where(gjl => gjl.UserId == guidedJournalLogPassed.UserId &&
                                                                    gjl.Date == guidedJournalLogPassed.Date);

        if (existsForSameDate.Any())
        {
            throw new SameDateException("Guided Journal Log", guidedJournalLogPassed.Date.ToString() ?? "");
        }

        _apiContext.GuidedJournalLogs.Add(guidedJournalLogPassed);

        _apiContext.SaveChanges(); 
    }

    public void Update(GuidedJournalLog guidedJournalLogPassed, Guid id)
    {

        var existingJournalLogPassed = _apiContext.GuidedJournalLogs.FirstOrDefault(gjl => gjl.Id == id && gjl.UserId == guidedJournalLogPassed.UserId);

        if (existingJournalLogPassed == null)
        {
            throw new NotFoundException("Journal Log");
        }
        else
        {
            existingJournalLogPassed.EntryTextValue = guidedJournalLogPassed.EntryTextValue;

            existingJournalLogPassed.Date = guidedJournalLogPassed.Date;

            _apiContext.SaveChanges();
        }

    }



}