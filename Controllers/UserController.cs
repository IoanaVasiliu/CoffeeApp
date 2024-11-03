using LibCafeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using LibCafeApp.Model;
namespace LibCafeApp.Controllers
{
    public class UserController
    {
    }


public static class UserEndpoints
{
	public static void MapUserEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/User").WithTags(nameof(User));

        group.MapGet("/", async (LibCafeAppContext db) =>
        {
            return await db.User.ToListAsync();
        })
        .WithName("GetAllUsers")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<User>, NotFound>> (int userid, LibCafeAppContext db) =>
        {
            return await db.User.AsNoTracking()
                .FirstOrDefaultAsync(model => model.UserId == userid)
                is User model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetUserById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int userid, User user, LibCafeAppContext db) =>
        {
            var affected = await db.User
                .Where(model => model.UserId == userid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.UserId, user.UserId)
                  .SetProperty(m => m.Username, user.Username)
                  .SetProperty(m => m.Password, user.Password)
                  .SetProperty(m => m.RoleName, user.RoleName)
                  .SetProperty(m => m.Email, user.Email)
                  .SetProperty(m => m.CreatedAt, user.CreatedAt)
                  .SetProperty(m => m.UpdatedAt, user.UpdatedAt)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateUser")
        .WithOpenApi();

        group.MapPost("/", async (User user, LibCafeAppContext db) =>
        {
            
            db.User.Add(user);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/User/{user.UserId}",user);
        })
        .WithName("CreateUser")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int userid, LibCafeAppContext db) =>
        {
            var affected = await db.User
                .Where(model => model.UserId == userid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteUser")
        .WithOpenApi();
    }
}}
