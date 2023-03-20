using Models; 

public interface IUserService
{
    List<User> GetAll(); 
    User? Get(int id); 
    void Add(User user); 
    void Update(User user); 
    void Delete(User user); 
    User? CheckUser(int id); 
    User? FindUserByEmail(string email);
    void VerifyPassword(User user, string password);  


}