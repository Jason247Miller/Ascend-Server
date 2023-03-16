using System; 
namespace Exceptions;

 public class DuplicateWellnessRatingException :Exception
{
    private readonly DateOnly _date; 
    public override string Message => $"A wellness rating for the date: '{_date}'  already exists.";

    public DuplicateWellnessRatingException(DateOnly date)
    {
        _date = date; 
    }

}

