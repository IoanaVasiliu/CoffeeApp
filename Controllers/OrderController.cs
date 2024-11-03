using LibCafeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using LibCafeApp.Model;
namespace LibCafeApp.Controllers
{
    public class OrderController
    {
    }


public static class OrderEndpoints
{
	public static void MapOrderEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Order").WithTags(nameof(Order));

        group.MapGet("/", async (LibCafeAppContext db) =>
        {
            return await db.Order.ToListAsync();
        })
        .WithName("GetAllOrders")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Order>, NotFound>> (int orderid, LibCafeAppContext db) =>
        {
            return await db.Order.AsNoTracking()
                .FirstOrDefaultAsync(model => model.OrderId == orderid)
                is Order model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetOrderById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int orderid, Order order, LibCafeAppContext db) =>
        {
            var affected = await db.Order
                .Where(model => model.OrderId == orderid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.OrderId, order.OrderId)
                  .SetProperty(m => m.UserId, order.UserId)
                  .SetProperty(m => m.OrderDate, order.OrderDate)
                  .SetProperty(m => m.TotalAmount, order.TotalAmount)
                  .SetProperty(m => m.StatusId, order.StatusId)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateOrder")
        .WithOpenApi();

        group.MapPost("/", async (Order order, LibCafeAppContext db) =>
        {
            db.Order.Add(order);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Order/{order.OrderId}",order);
        })
        .WithName("CreateOrder")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int orderid, LibCafeAppContext db) =>
        {
            var affected = await db.Order
                .Where(model => model.OrderId == orderid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteOrder")
        .WithOpenApi();
    }
}}
