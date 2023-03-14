using Models;
public interface IWellnessRatingService
{
    List<WellnessRating> GetAll();
    WellnessRating? Get(int id);
    void Add(WellnessRating wellnessRating);
    List<WellnessRating> GetAllForUserId(int userId);
    void Update(WellnessRating wellnessRating);
}