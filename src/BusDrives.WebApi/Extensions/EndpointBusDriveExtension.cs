using BusDrives.DataService.Repositories.Interfaces;
using BusDrives.Entities.DbSet;
using BusDrives.WebApi.Contracts;

namespace BusDrives.WebApi.Extensions;

public static class EndpointBusDriveExtension
{
    public static void AddBusDriveEndpoints(this IEndpointRouteBuilder builder)
    {
        //GET: /busdrives/
        builder.MapGet("/busdrives/", async (IBusDriveRepository repository) =>
        {
            return Results.Ok(await repository.GetAllAsync());
        });

        //GET: /busdrives/{id:int}
        builder.MapGet("/busdrives/{id:int}", async (int id, IBusDriveRepository repository) =>
        {
            var busdrive = await repository.GetByIdAsync(id);

            if (busdrive is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(busdrive);
        });

        //POST: /busdrives/
        builder.MapPost("/busdrives/", async (BusDriveRequest request, IBusDriveRepository repository) =>
        {
            var busdrive = new BusDrive
            {
                DriverCompany = request.DriverCompany,
                DepartureTime = request.DepartureTime,
                ArrivalTime = request.ArrivalTime,
                StartingCityId = request.StartingCityId,
                FinalCityId = request.FinalCityId,
                IsRecurring = request.IsRecurring,
                RecurrenceInterval = request.RecurrenceInterval,
                RecurrenceEndDate = request.RecurrenceEndDate
            };

            if (busdrive is null)
            {
                return Results.BadRequest();
            }

            await repository.AddAsync(busdrive);
            return Results.Created($"/busdrives/{busdrive.Id}", busdrive);
        });

        //PUT: /busdrives/{id:int}
        builder.MapPut("/busdrives/{id:int}", async (int id, BusDriveRequest request, IBusDriveRepository repository) =>
        {
            var busdrive = await repository.GetByIdAsync(id);

            if (busdrive is null)
            {
                return Results.NotFound();
            }

            busdrive.DriverCompany = request.DriverCompany;
            busdrive.ArrivalTime = request.ArrivalTime;
            busdrive.DepartureTime = request.DepartureTime;
            busdrive.StartingCityId = request.StartingCityId;
            busdrive.FinalCityId = request.FinalCityId;
            busdrive.RecurrenceInterval = request.RecurrenceInterval;
            busdrive.RecurrenceEndDate = request.RecurrenceEndDate;

            await repository.UpdateAsync(busdrive);

            return Results.NoContent();
        });

        //Delete: /busdrives/{id:int}
        builder.MapDelete("/busdrives/{id:int}", async (int id, IBusDriveRepository repository) =>
        {
            if (!await repository.ExistsAsync(id))
            {
                return Results.NotFound();
            }

            await repository.DeleteAsync(id);

            return Results.NoContent();
        });

        //GET: /busdrives/date/{date}
        builder.MapGet("/busdrives/date/{date}", async (DateTime date, IBusDriveRepository repository) =>
        {
            return Results.Ok(await repository.GetByDateAsync(date));
        });

        //GET: /busdrives/range
        builder.MapGet("/busdrives/range", async (DateTime startDate, DateTime endDate, IBusDriveRepository repository) =>
        {
            return Results.Ok(await repository.GetByDateRangeAsync(startDate, endDate));
        });
    }
}