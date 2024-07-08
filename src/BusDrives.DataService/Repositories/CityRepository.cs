using BusDrives.DataService.Data;
using BusDrives.DataService.Repositories.Interfaces;
using BusDrives.Entities.DbSet;
using Microsoft.EntityFrameworkCore;

namespace BusDrives.DataService.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AppDbContext _context;

    public CityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(City city)
    {
        await _context.Cities.AddAsync(city);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var city = await _context.Cities.FindAsync(id);
        if (city != null)
        {
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Cities.AnyAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        return await _context.Cities.ToListAsync();
    }

    public async Task<City?> GetByIdAsync(int id)
    {

        return await _context.Cities.FindAsync(id);
    }

    public async Task UpdateAsync(City city)
    {
        _context.Entry(city).State = EntityState.Modified;
            await _context.SaveChangesAsync();
    }
}