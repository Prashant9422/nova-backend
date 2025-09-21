using Microsoft.EntityFrameworkCore;
using NovaApp.Data;

namespace NovaApp.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbset;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbset = _context.Set<T>();
    }

    // Corrected the GetAllAsync method to match the interface
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbset.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbset.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbset.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbset.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbset.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
