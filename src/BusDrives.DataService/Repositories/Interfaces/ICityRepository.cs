using BusDrives.Entities.DbSet;

namespace BusDrives.DataService.Repositories.Interfaces;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllAsync(); 
    Task<City?> GetByIdAsync(int id);   
    Task AddAsync(City city);
    Task UpdateAsync(City city);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}