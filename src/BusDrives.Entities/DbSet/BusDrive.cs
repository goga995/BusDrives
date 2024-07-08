namespace BusDrives.Entities.DbSet;

public class BusDrive
{
    public int Id { get; set; }

    public string? DriverCompany { get; set; }

    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }

    public int StartingCityId { get; set; }
    public City? StartingCity { get; set; }

    public int FinalCityId { get; set; }
    public City? FinalCity { get; set; }

    public bool IsRecurring { get; set; }
    public string RecurrenceInterval {get; set;} = string.Empty; //Daily, Weakly
    public DateTime? RecurrenceEndDate { get; set; }

    public TimeSpan DriveDuration => ArrivalTime - DepartureTime;
}
