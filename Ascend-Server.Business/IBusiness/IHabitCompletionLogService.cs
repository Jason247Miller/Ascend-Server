using Data;

namespace IServices;
public interface IHabitCompletionLogService
{
    IEnumerable<HabitCompletionLog> GetAllForUserId(Guid userId);
    void Add(HabitCompletionLog habitCompletionLog);
    HabitCompletionLog GetById(Guid id);
    void Update(HabitCompletionLog habitCompletionLogPassed, Guid id);
}