using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class WellnessRatingForCreation
    {
        public string? Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Range(1, 10)]
        public int SleepRating { get; set; }
        [Range(1, 10)]
        public int ExerciseRating { get; set; }
        [Range(1, 10)]
        public int NutritionRating { get; set; }
        [Range(1, 10)]
        public int StressRating { get; set; }
        [Range(1, 10)]
        public int SunlightRating { get; set; }
        [Range(1, 10)]
        public int MindfulnessRating { get; set; }
        [Range(1, 10)]
        public int ProductivityRating { get; set; }
        [Range(1, 10)]
        public int MoodRating { get; set; }
        [Range(1, 10)]
        public int EnergyRating { get; set; }
        [Range(1, 10)]
        public int OverallDayRating { get; set; }
    }
}
