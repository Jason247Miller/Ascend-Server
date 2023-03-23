using Models;
public interface IWellnessRatingService
{
    void Add(WellnessRating wellnessRating);
    WellnessRating[] GetAllForUserId(Guid userId);
    void Update(WellnessRating wellnessRating, Guid id);
}