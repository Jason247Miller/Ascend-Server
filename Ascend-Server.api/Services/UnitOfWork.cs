using Ascend_Server.api.IConfiguration;
using Models;
using Services;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApiContext _apiContext;

    private readonly IUserService _userService;

    public IHabitService Habits { get; private set; }

    public UnitOfWork(ApiContext apiContext, IUserService userService)
    {
        _apiContext = apiContext;
        _userService = userService;

        Habits = new HabitService(_apiContext, _userService);
    }

    public async Task CompleteAsync()
    {
        await _apiContext.SaveChangesAsync();
    }

    public async void Dispose()
    {
        await _apiContext.DisposeAsync();
    }
}
