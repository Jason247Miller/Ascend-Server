using Models;
public interface IWellnessRatingService
{
    void Add(WellnessRating wellnessRating);
    List<WellnessRating> GetAllForUserId(int userId);
    void Update(WellnessRating wellnessRating);
}