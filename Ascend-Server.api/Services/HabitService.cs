using Models;
using Exceptions;
using Ascend_Server.api.Services;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class HabitService : GenericService<Habit>, IHabitService
{
    public HabitService(
        ApiContext apiContext,
        IUserService userService) : base(apiContext, userService)
    { }
    public async Task<Habit[]> GetAllForUserId(Guid userIdPassed)
    {
        _userService.CheckUserId(userIdPassed);

        var userHabits = dbSet.Where(h => h.UserId == userIdPassed).ToArrayAsync();

        return await userHabits;
    }

    public override async Task Add(Habit habit)
    {
        _userService.CheckUserId(habit.UserId);

        var habitsForUser = dbSet.SingleOrDefault(h => h.UserId == habit.UserId && h.Id == habit.Id);

        if (habitsForUser != null)
        {
            throw new DuplicateHabitException();
        }
        await dbSet.AddAsync(habit);

    }

    public void Update(Habit habit, Guid id)
    {
        var existingHabit = dbSet.SingleOrDefault(h => h.Id == id &&
                                                  h.UserId == habit.UserId);
        if (existingHabit == null)
        {
            throw new NotFoundException("Habit");
        }
        else
        {
            existingHabit.Deleted = habit.Deleted;

            existingHabit.HabitName = habit.HabitName;

            dbSet.Update(existingHabit);

        }

    }

}