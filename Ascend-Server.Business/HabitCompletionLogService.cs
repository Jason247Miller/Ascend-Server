using Data;
using Data.Exceptions;
using IServices;

namespace Services;

public class HabitCompletionLogService : IHabitCompletionLogService
{

    private readonly ApiContext _apiContext;

    public HabitCompletionLogService(IUserService userService,
                                     ApiContext apiContext)
    {
        _apiContext = apiContext;

    }

    public IEnumerable<Data.HabitCompletionLog> GetAllForUserId(Guid userId)
    {
        var userHabitCompletionLogs = _apiContext.HabitCompletionLogs.Where(hcl => hcl.UserId == userId);

        return userHabitCompletionLogs;
    }
    public HabitCompletionLog GetById(Guid id)
    {
        var existingLog = _apiContext.HabitCompletionLogs.SingleOrDefault(hcl => hcl.Id == id);

        if (existingLog == null)
        {
            throw new NotFoundException("Habit Completion Log");
        }

        return existingLog;
    }
    public void Add(HabitCompletionLog habitCompletionLogPassed)
    {

        var habitEntryForLog = _apiContext.Habits.FirstOrDefault(h => h.Id == habitCompletionLogPassed.HabitId &&
                                                                                 h.Deleted == false &&
                                                                                 h.UserId == habitCompletionLogPassed.UserId);
        if (habitEntryForLog == null)
        {
            throw new NotFoundException("Habit Entry For Log");
        }

        var existsForSameDate = _apiContext.HabitCompletionLogs.FirstOrDefault(hcl => hcl.UserId == habitCompletionLogPassed.UserId &&
                                                                               hcl.Date == habitCompletionLogPassed.Date &&
                                                                               hcl.HabitId == habitEntryForLog.Id);

        if (existsForSameDate != null)
        {
            throw new SameDateException("Habit Log", habitCompletionLogPassed.Date.ToString() ?? "");
        }

        _apiContext.HabitCompletionLogs.Add(habitCompletionLogPassed);

        _apiContext.SaveChanges();
    }

    public void Update(HabitCompletionLog habitCompletionLogPassed, Guid id)
    {
        var existingHabitCompletionLog = _apiContext.HabitCompletionLogs.SingleOrDefault(
            hcl => hcl.Id == id &&
            hcl.UserId == habitCompletionLogPassed.UserId
            );

        if (existingHabitCompletionLog == null)
        {
            throw new NotFoundException("Habit Completion Log");
        }
        else
        {
            existingHabitCompletionLog.Completed = habitCompletionLogPassed.Completed;

            existingHabitCompletionLog.Date = habitCompletionLogPassed.Date;

            _apiContext.SaveChanges();
        }

    }

}