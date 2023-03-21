using Models;
public interface IWellnessRatingService
{
    void Add(WellnessRating wellnessRating);
    List<WellnessRating> GetAllForUserId(Guid userId);
    void Update(WellnessRating wellnessRating);
}