using Models;
using Exceptions;
using System;
using Services;

namespace Services;

public class HabitService : IHabitService
{

    private readonly ApiContext _apiContext;

    private readonly IUserService _userService;

    public HabitService(
        IUserService userService,
        ApiContext apiContext)
    {
        _apiContext = apiContext;
        _userService = userService;
    }

    public List<Habit> GetAllForUserId(Guid userIdPassed)
    {

        _userService.CheckUserId(userIdPassed);

        List<Habit> userHabits = _apiContext.Habits.Where(h => h.UserId == userIdPassed).ToList();

        return userHabits;
    }

    public void Add(Habit habit)
    {

        _userService.CheckUserId(habit.UserId);

        var habitsForUser = _apiContext.Habits.Where(h => h.UserId == habit.UserId && h.Id == habit.Id);


        if (habitsForUser.Any())
        {
            throw new DuplicateHabitException();
        }

        _apiContext.Habits.Add(habit);
    }

    public void Update(Habit habit)
    {
        var existingHabit = _apiContext.Habits.SingleOrDefault(h => h.Id == habit.Id &&
                                                         h.UserId == habit.UserId);

        if (existingHabit == null)
        {
            throw new NotFoundException("Habit");
        }
        else
        {
            existingHabit.Deleted = habit.Deleted;

            existingHabit.HabitName = habit.HabitName;

            _apiContext.SaveChanges();

        }

    }


}