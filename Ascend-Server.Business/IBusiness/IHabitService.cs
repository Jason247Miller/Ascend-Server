using Data;

namespace IServices;

public interface IHabitService
{
    IEnumerable<Data.Habit> GetAllForUserId(Guid userId);
    void Add(Habit habit);
    void Update(Habit habit, Guid id);
    void Delete(Guid id);
    Habit GetById(Guid id);
}