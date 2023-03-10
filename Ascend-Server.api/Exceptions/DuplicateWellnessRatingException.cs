using System; 
namespace Exceptions;

 public class DuplicateWellnessRatingException :Exception
{
    private readonly DateTime _date; 
    public override string Message => $"A wellness rating for the date: '{_date}'  already exists.";

    public DuplicateWellnessRatingException(DateTime date)
    {
        _date = date; 
    }

}

