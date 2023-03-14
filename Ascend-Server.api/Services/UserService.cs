using Models; 
using Exceptions; 
using System; 

namespace Services; 

public class UserService : IUserService
{
   List<User> Users {get;}
   int nextId = 3; 

   public UserService()
   {
        Users = new List<User>
        {
            new User
            {
            Id = 1, 
            FirstName = "Jason", 
            LastName = "Miller", 
            Email = "jason.miller@gmail.com",
            Password = "testPass123!"
            },
            new User 
            {
            Id = 2, 
            FirstName = "Jim", 
            LastName = "Morrison",
            Email = "jim.morrison@yahoo.com",
            Password = "testPass123!"
            }
        };
    }

    public List<User> GetAll() => Users; 

    public  User? Get(int id) => Users.FirstOrDefault(u => u.Id == id);

    public void Add(User user)
    {   
        var userEmailExists = Users.FirstOrDefault(u => u.Email == user.Email);
        
        if(userEmailExists == null)
        {   
            throw new UserEmailExistsException(user);
        }
  
        user.Id = nextId++;
        Users.Add(user);
    }

    public void Update(User user)
    {
         var index = Users.FindIndex(u => u.Id == user.Id);
       
        if(index == 0)
        {  
            throw new NotFoundException(user); 
        }
        else
        {
            Users[index] = user;
        }
     
    }

    public void Delete(User user)
    {
        var index = Users.FindIndex(u => u.Id == user.Id);
        
        if(index == 0)
        {
            throw new NotFoundException(user); 
        }
        else
        {
         Users.RemoveAt(index); 
        }
        
    }
 
public User CheckUser(int id)
{   
    var user = Users.FirstOrDefault(u => u.Id == id);
    
    return user as User; 
}
}