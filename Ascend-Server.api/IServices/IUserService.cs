using Models;

public interface IUserService
{
    User? Get(Guid id);
    void Add(User user);
    void Update(User user);
    void Delete(User user);
    User? FindUserByEmail(string email);
    bool VerifyPassword(User user, string password);
    void CheckUserId(Guid userIdPassed);

}