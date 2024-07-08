namespace BusDrives.WebApi.Contracts;

public record BusDriveRequest(string DriverCompany,
                              DateTime DepartureTime,
                              DateTime ArrivalTime,
                              int StartingCityId,
                              int FinalCityId,
                              bool IsRecurring,
                              string RecurrenceInterval,
                              DateTime? RecurrenceEndDate);
