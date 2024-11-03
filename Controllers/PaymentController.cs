using LibCafeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using LibCafeApp.Model;
namespace LibCafeApp.Controllers
{
    public class PaymentController
    {
    }


public static class PaymentEndpoints
{
	public static void MapPaymentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Payment").WithTags(nameof(Payment));

        group.MapGet("/", async (LibCafeAppContext db) =>
        {
            return await db.Payment.ToListAsync();
        })
        .WithName("GetAllPayments")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Payment>, NotFound>> (int paymentid, LibCafeAppContext db) =>
        {
            return await db.Payment.AsNoTracking()
                .FirstOrDefaultAsync(model => model.PaymentId == paymentid)
                is Payment model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPaymentById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int paymentid, Payment payment, LibCafeAppContext db) =>
        {
            var affected = await db.Payment
                .Where(model => model.PaymentId == paymentid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.PaymentId, payment.PaymentId)
                  .SetProperty(m => m.OrderId, payment.OrderId)
                  .SetProperty(m => m.Amount, payment.Amount)
                  .SetProperty(m => m.PaymentMethod, payment.PaymentMethod)
                  .SetProperty(m => m.PaymentStatus, payment.PaymentStatus)
                  .SetProperty(m => m.PaymentDate, payment.PaymentDate)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePayment")
        .WithOpenApi();

        group.MapPost("/", async (Payment payment, LibCafeAppContext db) =>
        {
            db.Payment.Add(payment);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Payment/{payment.PaymentId}",payment);
        })
        .WithName("CreatePayment")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int paymentid, LibCafeAppContext db) =>
        {
            var affected = await db.Payment
                .Where(model => model.PaymentId == paymentid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePayment")
        .WithOpenApi();
    }
}}
