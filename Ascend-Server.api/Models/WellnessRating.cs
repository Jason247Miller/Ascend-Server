namespace Models; 

public class WellnessRating
{
    public int Id {get;set;}
    public int UserId {get;set;}
    public string? date {get; set;}//will change to DateTime obj
    public int sleepRating {get; set;}
    public int exerciseRating {get; set;}
    public int nutritionRating {get; set;}
    public int stressRating {get; set;}
    public int sunlightRating {get; set;}
    public int mindfulnessRating {get; set;}
    public int productivityRating {get; set;}
    public int moodRating {get; set;}
    public int energyRating {get; set;}
    public int overallDayRating {get; set;}
}