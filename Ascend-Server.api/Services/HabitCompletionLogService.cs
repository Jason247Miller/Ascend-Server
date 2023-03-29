using Models;
using Exceptions;
using System;
using Services;

namespace Services;

public class HabitCompletionLogService : IHabitCompletionLogService
{
    private readonly IUserService _userService;

    private readonly ApiContext _apiContext;

    public HabitCompletionLogService(IUserService userService,
                                     ApiContext apiContext)
    {
        _userService = userService;

        _apiContext = apiContext;

    }

    public HabitCompletionLog[] GetAllForUserId(Guid userId)
    {
        _userService.CheckUserId(userId);

        var userHabitCompletionLogs = _apiContext.HabitCompletionLogs.Where(hcl => hcl.UserId == userId).ToArray();

        return userHabitCompletionLogs;
    }

    public void Add(HabitCompletionLog habitCompletionLogPassed)
    {
        _userService.CheckUserId(habitCompletionLogPassed.UserId);

        var habitForLog = _apiContext.Habits.FirstOrDefault(h => h.Id == habitCompletionLogPassed.HabitId &&
                                                                                 h.Deleted == false &&
                                                                                 h.UserId == habitCompletionLogPassed.UserId);
        if (habitForLog == null)
        {
            throw new NotFoundException("Habit");
        }

        var existsForSameDate = _apiContext.HabitCompletionLogs.FirstOrDefault(hcl => hcl.UserId == habitCompletionLogPassed.UserId &&
                                                                      hcl.Date == habitCompletionLogPassed.Date);

        if (existsForSameDate == null)
        {
            throw new SameDateException("Habit Log", habitCompletionLogPassed.Date.ToString() ?? "");
        }

        _apiContext.HabitCompletionLogs.Add(habitCompletionLogPassed);

        _apiContext.SaveChanges();
    }

    public void Update(HabitCompletionLog habitCompletionLogPassed, Guid id)
    {

        var existingHabitCompletionLog = _apiContext.HabitCompletionLogs.FirstOrDefault(
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
        }

    }

}