using BusDrives.Entities.DbSet;

namespace BusDrives.DataService.Repositories.Interfaces;

public interface IBusDriveRepository
{
    Task<IEnumerable<BusDrive>> GetAllAsync();
    Task<BusDrive?> GetByIdAsync(int id);
    Task AddAsync(BusDrive busDrive);
    Task UpdateAsync(BusDrive busDrive);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<BusDrive>> GetByDateAsync(DateTime date);
    Task<IEnumerable<BusDrive>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
}