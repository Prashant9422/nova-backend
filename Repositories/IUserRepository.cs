using NovaApp.Models;

namespace NovaApp.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(int id);
    Task<User> AddUserAsync(User user);
}