using LibCafeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using LibCafeApp.Model;
namespace LibCafeApp.Controllers
{
    public class ReviewController
    {
    }


public static class ReviewEndpoints
{
	public static void MapReviewEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Review").WithTags(nameof(Review));

        group.MapGet("/", async (LibCafeAppContext db) =>
        {
            return await db.Review.ToListAsync();
        })
        .WithName("GetAllReviews")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Review>, NotFound>> (int reviewid, LibCafeAppContext db) =>
        {
            return await db.Review.AsNoTracking()
                .FirstOrDefaultAsync(model => model.ReviewId == reviewid)
                is Review model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetReviewById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int reviewid, Review review, LibCafeAppContext db) =>
        {
            var affected = await db.Review
                .Where(model => model.ReviewId == reviewid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.ReviewId, review.ReviewId)
                  .SetProperty(m => m.UserId, review.UserId)
                  .SetProperty(m => m.Rating, review.Rating)
                  .SetProperty(m => m.Comment, review.Comment)
                  .SetProperty(m => m.ReviewDate, review.ReviewDate)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateReview")
        .WithOpenApi();

        group.MapPost("/", async (Review review, LibCafeAppContext db) =>
        {
            db.Review.Add(review);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Review/{review.ReviewId}",review);
        })
        .WithName("CreateReview")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int reviewid, LibCafeAppContext db) =>
        {
            var affected = await db.Review
                .Where(model => model.ReviewId == reviewid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteReview")
        .WithOpenApi();
    }
}}
