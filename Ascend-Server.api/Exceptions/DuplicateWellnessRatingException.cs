using System; 
namespace Exceptions;

 public class DuplicateWellnessRatingException :Exception
{
    private readonly string _date; 
    public override string Message => "A post with date:" + _date + " already exists.";

    public DuplicateWellnessRatingException(string date)
    {
        _date = date; 
    }

}

