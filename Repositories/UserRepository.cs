using Microsoft.EntityFrameworkCore;
using NovaApp.Data;
using NovaApp.Models;

namespace NovaApp.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    public UserRepository(ApplicationDbContext db) => _db = db;
    public async Task<User> AddUserAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        return user;
    }
    public async Task<User?> GetByIdAsync(int id) => await _db.Users.FindAsync(id);
    public async Task<User?> GetByUsernameAsync(string username) => await _db.Users.SingleOrDefaultAsync(u => u.Username == username);
}