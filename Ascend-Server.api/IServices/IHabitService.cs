using Ascend_Server.api.IServices;
using Models;

public interface IHabitService : IGenericService<Habit>
{
    Task<Habit[]> GetAllForUserId(Guid userId);
    void Update(Habit habit, Guid userId);
}