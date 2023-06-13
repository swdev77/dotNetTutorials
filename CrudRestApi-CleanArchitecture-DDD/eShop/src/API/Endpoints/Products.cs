using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.Get;
using Application.Products.Update;
using Carter;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

public class Products : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("products", 
            async (CreateProductCommand command, ISender sender) =>
            {
                await sender.Send(command);
                return Results.Ok();
            });

        app.MapGet("products/{id:guid}", 
            async (Guid id, ISender sender) =>
            {
                try
                {
                    return Results.Ok(await sender.Send(new GetProductQuery(new ProductId(id))));
                }
                catch(ProductNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
            });

        app.MapPut("products/{id:guid}", 
            async (
                Guid id,
                [FromBody] UpdateProductRequest request,
                ISender sender) =>
            {
                var command = new UpdateProductCommand(
                    ProductId: new ProductId(id),
                    Name: request.Name,
                    Sku: request.Sku,
                    Currency: request.Currency,
                    Amount: request.Amount);

                try
                {
                    await sender.Send(command);
                    return Results.NoContent();
                }
                catch(ProductNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
            });

        app.MapDelete("products/{id:guid}", 
            async (Guid id, ISender sender) =>
            {
                try
                {
                    await sender.Send(new DeleteProductCommand(new ProductId(id)));
                    return Results.NoContent();
                }
                catch(ProductNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
            });
    }
}
