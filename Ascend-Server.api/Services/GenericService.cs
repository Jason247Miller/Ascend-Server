using Ascend_Server.api.IServices;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

namespace Ascend_Server.api.Services;

public class GenericService<T> : IGenericService<T> where T : class
{
    protected readonly ApiContext _apiContext;

    protected readonly IUserService _userService;

    protected DbSet<T> dbSet;

    public GenericService(ApiContext apiContext, IUserService userService)
    {
        _apiContext = apiContext;

        _userService = userService;

        dbSet = _apiContext.Set<T>();
    }

    public virtual async Task Add(T entity)
    {
        await dbSet.AddAsync(entity);

    }

    public virtual void Update(T entity)
    {
        dbSet.Update(entity);
    }

}

