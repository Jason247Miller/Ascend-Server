
namespace Data.Exceptions;

public class SameDateException : Exception
{
    private readonly string _entity;

    private readonly string _date;

    public override string Message => $"'{_entity}' Already Exists for the date of '{_date}' ";

    public SameDateException(string entity, string date)
    {
        _entity = entity;
        _date = date;
    }
}