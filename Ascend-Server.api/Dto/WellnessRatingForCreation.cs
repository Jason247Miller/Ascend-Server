namespace Ascend_Server.api.Dto;

public class WellnessRatingForCreation
{
    public string? Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public int SleepRating { get; set; }
    public int ExerciseRating { get; set; }
    public int NutritionRating { get; set; }
    public int StressRating { get; set; }
    public int SunlightRating { get; set; }
    public int MindfulnessRating { get; set; }
    public int ProductivityRating { get; set; }
    public int MoodRating { get; set; }
    public int EnergyRating { get; set; }
    public int OverallDayRating { get; set; }

}