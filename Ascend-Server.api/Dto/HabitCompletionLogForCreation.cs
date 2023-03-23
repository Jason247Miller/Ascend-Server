namespace Ascend_Server.api.Dto;

public class HabitCompletionLogForCreation
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid HabitId { get; set; }
    public bool? Completed { get; set; }
    public DateOnly? Date { get; set; }
}
