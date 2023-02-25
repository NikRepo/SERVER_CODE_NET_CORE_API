using Microsoft.EntityFrameworkCore;
using CommonServiceHelper.Service_Model;
using ProductService.Data;
namespace ProductService.Controllers;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Order", async (ProductServiceContext db) =>
        {
            return await db.Orders.ToListAsync();
        })
        .WithName("GetAllOrders")
        .Produces<List<Order>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Order/{id}", async (string OrderId, ProductServiceContext db) =>
        {
            return await db.Orders.FindAsync(OrderId)
                is Order model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetOrderById")
        .Produces<Order>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Order/{id}", async (string OrderId, Order order, ProductServiceContext db) =>
        {
            var foundModel = await db.Orders.FindAsync(OrderId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            
            db.Update(order);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateOrder")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Order/", async (Order order, ProductServiceContext db) =>
        {
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return Results.Created($"/Orders/{order.OrderId}", order);
        })
        .WithName("CreateOrder")
        .Produces<Order>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Order/{id}", async (string OrderId, ProductServiceContext db) =>
        {
            if (await db.Orders.FindAsync(OrderId) is Order order)
            {
                db.Orders.Remove(order);
                await db.SaveChangesAsync();
                return Results.Ok(order);
            }

            return Results.NotFound();
        })
        .WithName("DeleteOrder")
        .Produces<Order>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
