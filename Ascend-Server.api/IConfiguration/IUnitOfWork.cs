namespace Ascend_Server.api.IConfiguration
{
    public interface IUnitOfWork
    {
        IHabitService Habits { get;}

        Task CompleteAsync();//sends changes back to db
    }
}
