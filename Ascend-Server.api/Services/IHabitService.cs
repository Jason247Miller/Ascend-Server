using Models;

public interface IHabitService
{
    Habit[] GetAllForUserId(Guid userId);
    void Add(Habit habit);
    void Update(Habit habit, Guid id);
}