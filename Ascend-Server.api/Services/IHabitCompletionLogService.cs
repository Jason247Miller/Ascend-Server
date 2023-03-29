using Models;

public interface IHabitCompletionLogService
{
    HabitCompletionLog[] GetAllForUserId(Guid userId);
    void Add(HabitCompletionLog habitCompletionLog);
    void Update(HabitCompletionLog habitCompletionLogPassed, Guid id);
}