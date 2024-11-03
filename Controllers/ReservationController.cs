using LibCafeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using LibCafeApp.Model;
namespace LibCafeApp.Controllers
{
    public class ReservationController
    {
    }


public static class ReservationEndpoints
{
	public static void MapReservationEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Reservation").WithTags(nameof(Reservation));

        group.MapGet("/", async (LibCafeAppContext db) =>
        {
            return await db.Reservation.ToListAsync();
        })
        .WithName("GetAllReservations")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Reservation>, NotFound>> (int reservationid, LibCafeAppContext db) =>
        {
            return await db.Reservation.AsNoTracking()
                .FirstOrDefaultAsync(model => model.ReservationId == reservationid)
                is Reservation model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetReservationById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int reservationid, Reservation reservation, LibCafeAppContext db) =>
        {
            var affected = await db.Reservation
                .Where(model => model.ReservationId == reservationid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.ReservationId, reservation.ReservationId)
                  .SetProperty(m => m.UserId, reservation.UserId)
                  .SetProperty(m => m.TableId, reservation.TableId)
                  .SetProperty(m => m.ReservationDate, reservation.ReservationDate)
                  .SetProperty(m => m.Status, reservation.Status)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateReservation")
        .WithOpenApi();

        group.MapPost("/", async (Reservation reservation, LibCafeAppContext db) =>
        {
            db.Reservation.Add(reservation);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Reservation/{reservation.ReservationId}",reservation);
        })
        .WithName("CreateReservation")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int reservationid, LibCafeAppContext db) =>
        {
            var affected = await db.Reservation
                .Where(model => model.ReservationId == reservationid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteReservation")
        .WithOpenApi();
    }
}}
