namespace Ascend_Server.api.Dto;

public class Habit
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public bool? Deleted { get; set; }
    public string? HabitName { get; set; }
}


