using Data;
using Data.Exceptions;
using IServices;

namespace Services;

public class HabitService : IHabitService
{
    private readonly ApiContext _apiContext;

    public HabitService(
        ApiContext apiContext)
    {
        _apiContext = apiContext;
    }

    public IEnumerable<Data.Habit> GetAllForUserId(Guid userIdPassed)
    {
        var userHabits = _apiContext.Habits.Where(h => h.UserId == userIdPassed);

        return userHabits;
    }
    public Habit GetById(Guid id)
    {
        var existingHabit = _apiContext.Habits.SingleOrDefault(h => h.Id == id);

        if (existingHabit == null)
        {
            throw new NotFoundException("Habit");
        }

        return existingHabit;
    }
    public void Add(Habit habit)
    {
        var habitLog = new HabitCompletionLog
        {
            Id = Guid.NewGuid(),
            UserId = habit.UserId,
            HabitId = habit.Id,
            Completed = false,
            Date = habit.CreationDate
        };

        _apiContext.Habits.Add(habit);

        _apiContext.HabitCompletionLogs.Add(habitLog);

        _apiContext.SaveChanges();
    }

    public void Update(Habit habit, Guid id)
    {
        var existingHabit = _apiContext.Habits.SingleOrDefault(h => h.Id == id &&
                                                               h.UserId == habit.UserId);

        if (existingHabit == null)
        {
            throw new NotFoundException("Habit");
        }
        else
        {

            existingHabit.HabitName = habit.HabitName;

            _apiContext.SaveChanges();
        }

    }

    public void Delete(Guid id)
    {
        var existingHabit = _apiContext.Habits.SingleOrDefault(h => h.Id == id);

        if (existingHabit == null)
        {
            throw new NotFoundException("Journal Entry");
        }

            existingHabit.Deleted = true;

            _apiContext.SaveChanges();
    }
}