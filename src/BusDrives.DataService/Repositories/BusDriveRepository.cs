using BusDrives.DataService.Data;
using BusDrives.DataService.Repositories.Interfaces;
using BusDrives.Entities.DbSet;
using Microsoft.EntityFrameworkCore;

namespace BusDrives.DataService.Repositories;

public class BusDriveRepository : IBusDriveRepository
{
    private readonly AppDbContext _context;

    public BusDriveRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task AddAsync(BusDrive busDrive)
    {
        await _context.BusDrives.AddAsync(busDrive);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var busDrive = await _context.BusDrives.FindAsync(id);
        if (busDrive != null)
        {
            _context.BusDrives.Remove(busDrive);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.BusDrives.AnyAsync(bd => bd.Id == id);
    }

    public async Task<IEnumerable<BusDrive>> GetAllAsync()
    {
        return await _context.BusDrives
            .Include(bd => bd.StartingCity)
            .Include(bd => bd.FinalCity)
            .ToListAsync();
    }

    public async Task<IEnumerable<BusDrive>> GetByDateAsync(DateTime date)
    {
        return await _context.BusDrives
                .Include(bd => bd.StartingCity)
                .Include(bd => bd.FinalCity)
                .Where(bd => bd.DepartureTime.Date == date.Date)
                .ToListAsync();
    }

    public async Task<IEnumerable<BusDrive>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var drives = await _context.BusDrives
                .Include(bd => bd.StartingCity)
                .Include(bd => bd.FinalCity)
                .ToListAsync();

        var result = new List<BusDrive>();

        foreach (var drive in drives)
        {
            if (!drive.IsRecurring)
            {
                if (drive.DepartureTime.Date >= startDate.Date && drive.DepartureTime.Date <= endDate.Date)
                {
                    result.Add(drive);
                }
            }
            else
            {
                var current = drive.DepartureTime;
                while (current <= endDate && (!drive.RecurrenceEndDate.HasValue || current <= drive.RecurrenceEndDate.Value))
                {
                    if (current.Date >= startDate.Date && current.Date <= endDate.Date)
                    {
                        var newDrive = new BusDrive
                        {
                            Id = drive.Id,
                            DepartureTime = current,
                            ArrivalTime = drive.ArrivalTime.Add(current - drive.DepartureTime),
                            StartingCityId = drive.StartingCityId,
                            StartingCity = drive.StartingCity,
                            FinalCityId = drive.FinalCityId,
                            FinalCity = drive.FinalCity,
                            IsRecurring = drive.IsRecurring,
                            RecurrenceInterval = drive.RecurrenceInterval,
                            RecurrenceEndDate = drive.RecurrenceEndDate
                        };
                        result.Add(newDrive);
                    }

                    current = drive.RecurrenceInterval switch
                    {
                        "Daily" => current.AddDays(1),
                        "Weekly" => current.AddDays(7),
                        _ => current
                    };
                }
            }
        }

        return result;
    }


    public async Task<BusDrive?> GetByIdAsync(int id)
    {
        return await _context.BusDrives
                .Include(bd => bd.StartingCity)
                .Include(bd => bd.FinalCity)
                .FirstOrDefaultAsync(bd => bd.Id == id);
    }

    public async Task UpdateAsync(BusDrive busDrive)
    {
        _context.Entry(busDrive).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}