using BLL.Interfaces;
using Core.Models;
using DAL.Contexts;

namespace BLL.Services;

public class UserService : IUserService
{
    private RestaurantContext _dbContext;

    public UserService(RestaurantContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public User GetUserById(int id)
    {
        return _dbContext.Users.Find(id) ?? throw new NullReferenceException();
    }
    
    public IQueryable<User> GetUsers(int count)
    {
        return _dbContext.Users.Take(count);
    }
}