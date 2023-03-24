using Models;
using Exceptions;
using System;
using Services;
namespace Services;

public class GuidedJournalLogService : IGuidedJournalLogService
{
    List<GuidedJournalLog> GuidedJournalLogs { get; }

    int nextId = 3;

    private readonly IUserService _userService;

    private readonly IGuidedJournalEntryService _guidedJournalEntryService;

    public GuidedJournalLogService(
       IUserService userService,
       IGuidedJournalEntryService guidedJournalEntryService
   )
    {
        _userService = userService;

        _guidedJournalEntryService = guidedJournalEntryService;

        GuidedJournalLogs = new List<GuidedJournalLog>
        {
            new GuidedJournalLog {
                Id = 1,
                UserId = 1,
                EntryId = "2e2bd1d4-c4a3-475a-bc8a-5aea1156e0ec",
                EntryTextValue = "text here",
                Date = new DateOnly(2023, 03, 16)
            },

            new GuidedJournalLog {
                Id = 2,
                UserId = 2,
                EntryId = "d58a9560-3ed8-4eaa-b97e-c558179861e9",
                EntryTextValue = "text here",
                Date = new DateOnly(2023, 03, 16)
            }

        };
    }

    public List<GuidedJournalLog> GetAllForUserId(int userId)
    {
        if (_userService.CheckUser(userId) == null)
        {
            throw new Exception("Invalid Request");
        }

        List<GuidedJournalLog> userGuidedJournalLogs = GuidedJournalLogs.Where(gjl => gjl.UserId == userId).ToList();

        return userGuidedJournalLogs;
    }

    public void Add(GuidedJournalLog guidedJournalLog)
    {
        if (_userService.CheckUser(guidedJournalLog.UserId) == null)
        {
            throw new UserDoesNotExistException(guidedJournalLog.UserId);
        }
        var journalLogFound = _guidedJournalEntryService.EntryExistsAndNotDeleted(guidedJournalLog.EntryId, guidedJournalLog.UserId);

        if (!journalLogFound)
        {
            throw new JournalEntryNotFoundException();
        }

        guidedJournalLog.Id = nextId++;

        GuidedJournalLogs.Add(guidedJournalLog);
    }

    public void Update(GuidedJournalLog guidedJournalLog)
    {

        var index = GuidedJournalLogs.FindIndex(gjl => gjl.Id == guidedJournalLog.Id && gjl.UserId == guidedJournalLog.UserId);

        if (index == -1)
        {
            throw new NotFoundException(guidedJournalLog);
        }
        else
        {
            GuidedJournalLogs[index] = guidedJournalLog;
        }

    }

}