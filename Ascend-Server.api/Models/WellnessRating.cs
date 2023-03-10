namespace Models; 

public class WellnessRating
{
    public int Id {get;set;}
    public int UserId {get;set;}
    public DateTime Date {get; set;}
    public int SleepRating {get; set;}
    public int ExerciseRating {get; set;}
    public int NutritionRating {get; set;}
    public int StressRating {get; set;}
    public int SunlightRating {get; set;}
    public int MindfulnessRating {get; set;}
    public int ProductivityRating {get; set;}
    public int MoodRating {get; set;}
    public int EnergyRating {get; set;}
    public int OverallDayRating {get; set;}
}