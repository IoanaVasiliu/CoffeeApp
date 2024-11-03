using LibCafeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using LibCafeApp.Model;
namespace LibCafeApp.Controllers
{
    public class BookController
    {
    }


public static class BookEndpoints
{
	public static void MapBookEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Book").WithTags(nameof(Book));

        group.MapGet("/", async (LibCafeAppContext db) =>
        {
            return await db.Book.ToListAsync();
        })
        .WithName("GetAllBooks")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Book>, NotFound>> (int bookid, LibCafeAppContext db) =>
        {
            return await db.Book.AsNoTracking()
                .FirstOrDefaultAsync(model => model.BookId == bookid)
                is Book model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBookById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int bookid, Book book, LibCafeAppContext db) =>
        {
            var affected = await db.Book
                .Where(model => model.BookId == bookid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.BookId, book.BookId)
                  .SetProperty(m => m.Title, book.Title)
                  .SetProperty(m => m.Author, book.Author)
                  .SetProperty(m => m.Genre, book.Genre)
                  .SetProperty(m => m.StockQuantity, book.StockQuantity)
                  .SetProperty(m => m.Availability, book.Availability)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBook")
        .WithOpenApi();

        group.MapPost("/", async (Book book, LibCafeAppContext db) =>
        {
            db.Book.Add(book);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Book/{book.BookId}",book);
        })
        .WithName("CreateBook")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int bookid, LibCafeAppContext db) =>
        {
            var affected = await db.Book
                .Where(model => model.BookId == bookid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBook")
        .WithOpenApi();
    }
}}
