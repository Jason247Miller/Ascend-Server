using Models;
using Exceptions;
using System;

namespace Services;

public class UserService : IUserService
{
    private readonly ApiContext _apiContext;

    public UserService(ApiContext apiContext)
    {
        _apiContext = apiContext;
    }

    public User? Get(Guid id) => _apiContext.Users.FirstOrDefault(u => u.Id == id);

    public void Add(User userPassed)
    {
        var userEmailExists = _apiContext.Users.FirstOrDefault(u => u.Email == userPassed.Email);

        if (userEmailExists != null)
        {
            throw new Exception("User already Exists");
        }

        _apiContext.Users.Add(userPassed);

        _apiContext.SaveChanges();
    }

    public void Update(User userPassed)
    {
        var existingUser = _apiContext.Users.FirstOrDefault(u => u.Id == userPassed.Id);

        if (existingUser == null)
        {
            throw new NotFoundException("User");
        }
        else
        {
            existingUser.Email = userPassed.Email;
            existingUser.FirstName = userPassed.FirstName;
            existingUser.LastName = userPassed.LastName;
        }

    }

    public void Delete(User userPassed)
    {
        var existingUser = _apiContext.Users.FirstOrDefault(u => u.Id == userPassed.Id);

        if (existingUser == null)
        {
            throw new NotFoundException("User");
        }
        else
        {
            _apiContext.Users.Remove(existingUser);
        }
    }

    public User? FindUserByEmail(string email)
    {

        var user = _apiContext.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
        {
            throw new Exception();
        }

        return user as User;
    }

    public bool VerifyPassword(User user, string password)
    {
        if (user.Password != password)
        {
            return false;
        }
        return true;
    }
    public void CheckUserId(Guid userIdPassed)
    {
        var userId = _apiContext.Users.SingleOrDefault(u => u.Id == userIdPassed);

        if (userId == null)
        {
            throw new UserDoesNotExistException();
        }
    }
}