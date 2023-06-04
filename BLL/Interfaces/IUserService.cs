using Core.Models;

namespace BLL.Interfaces;

public interface IUserService
{
    User GetUserById(int id);
    
    IQueryable<User> GetUsers(int count);
}