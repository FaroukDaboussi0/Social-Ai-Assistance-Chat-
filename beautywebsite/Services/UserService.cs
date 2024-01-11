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
        // Assuming _context is your DbContext containing the Users DbSet
        return _context.Users.FirstOrDefault(u => u.Name == username);
    }
    public User getuserbyid(int id){
        User ?us = _context.Users.Find(id);
        return us;

    }
    
}