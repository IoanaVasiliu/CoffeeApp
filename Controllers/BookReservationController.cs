using LibCafeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using LibCafeApp.Model;
namespace LibCafeApp.Controllers
{
    public class BookReservationController
    {
    }


public static class BookReservationEndpoints
{
	public static void MapBookReservationEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/BookReservation").WithTags(nameof(BookReservation));

        group.MapGet("/", async (LibCafeAppContext db) =>
        {
            return await db.BookReservation.ToListAsync();
        })
        .WithName("GetAllBookReservations")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<BookReservation>, NotFound>> (int reservationid, LibCafeAppContext db) =>
        {
            return await db.BookReservation.AsNoTracking()
                .FirstOrDefaultAsync(model => model.ReservationId == reservationid)
                is BookReservation model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBookReservationById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int reservationid, BookReservation bookReservation, LibCafeAppContext db) =>
        {
            var affected = await db.BookReservation
                .Where(model => model.ReservationId == reservationid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.ReservationId, bookReservation.ReservationId)
                  .SetProperty(m => m.UserId, bookReservation.UserId)
                  .SetProperty(m => m.BookId, bookReservation.BookId)
                  .SetProperty(m => m.ReservationDate, bookReservation.ReservationDate)
                  .SetProperty(m => m.Status, bookReservation.Status)
                  .SetProperty(m => m.Stock, bookReservation.Stock)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBookReservation")
        .WithOpenApi();

        group.MapPost("/", async (BookReservation bookReservation, LibCafeAppContext db) =>
        {
            db.BookReservation.Add(bookReservation);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/BookReservation/{bookReservation.ReservationId}",bookReservation);
        })
        .WithName("CreateBookReservation")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int reservationid, LibCafeAppContext db) =>
        {
            var affected = await db.BookReservation
                .Where(model => model.ReservationId == reservationid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBookReservation")
        .WithOpenApi();
    }
}}
