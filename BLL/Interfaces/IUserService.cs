using Core.CredentialModels;
using Core.Models;

namespace BLL.Interfaces;

public interface IUserService
{
    User GetUserById(int id);
    
    IQueryable<User> GetUsers(int count);
    
    User? AddUser(User user);
    
    bool UpdateUser(User user);
}