using BusDrives.DataService.Repositories.Interfaces;
using BusDrives.Entities.DbSet;
using BusDrives.WebApi.Contracts;

namespace BusDrives.WebApi.EndpointExtension;

public static class EndpointCityExtension
{
    public static void AddCityEndpoints(this IEndpointRouteBuilder builder)
    {

        //GET /cities
        builder.MapGet("/cities/", async (ICityRepository repository) =>
        {
            var results = await repository.GetAllAsync();

            return Results.Ok(results);
        });

        //GET /cities/{id:int}
        builder.MapGet("/cities/{id:int}", async (int id, ICityRepository repository) =>
        {
            var result = await repository.GetByIdAsync(id);

            if (result is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(result);
        });

        //POST /cities/
        builder.MapPost("/cities/", async (CityRequest request, ICityRepository repository) =>
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return Results.BadRequest();
            }
            var city = new City { Name = request.Name };
            await repository.AddAsync(city);

            return Results.Created($"/cities/{city.Id}", city);
        });

        //PUT /cities/{id:int}
        builder.MapPut("/cities/{id:int}", async (int id, ICityRepository repository, CityRequest request) =>
        {
            var city = await repository.GetByIdAsync(id);

            if (string.IsNullOrEmpty(request.Name))
            {
                return Results.BadRequest();
            }

            if (city is null)
            {
                return Results.NotFound();
            }

            city.Name = request.Name;

            await repository.UpdateAsync(city);

            return Results.NoContent();
        });

        //DELETE /cities/{id:int}
        builder.MapDelete("/cities/{id:int}", async (int id, ICityRepository repository) =>
        {
            if(!await repository.ExistsAsync(id))
            {
                return Results.NotFound();
            }

            await repository.DeleteAsync(id);

            return Results.NoContent();
        });
    }

}