using System.Net.Mail;
using System.Text.RegularExpressions;
using BLL.Interfaces;
using Core.CredentialModels;
using Core.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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
    
    public User? AddUser(User user)
    {
        _dbContext.Users.Attach(user);
        
        _dbContext.SaveChanges();
        
        return user;
    }

    public bool UpdateUser(User user)
    {
        _dbContext.Users.Update(user);

        _dbContext.SaveChanges();

        return true;
    }
}