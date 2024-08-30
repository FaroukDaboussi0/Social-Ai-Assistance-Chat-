using System;
namespace beautywebsite.Services;
public class UserService
{
    private readonly MessagingDbContext _context ;
    public UserService(MessagingDbContext cx){
        _context=cx;
    }
    public User GetUserByName(string username)
    {
       
        return _context.Users.FirstOrDefault(u => u.Name == username);
    }
    public User getuserbyid(int id)
    {
        var us = _context.Users.Find(id);
        if (us == null)
        {
            Console.WriteLine($"User with ID {id} not found in the database.");
        }
        return us;
    }
}