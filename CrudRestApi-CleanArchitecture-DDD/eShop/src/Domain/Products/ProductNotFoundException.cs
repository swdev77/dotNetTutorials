namespace Domain.Products;

public sealed class ProductNotFoundException : Exception
{
    public ProductNotFoundException(ProductId productId) 
        : base($"Product with id {productId} was not found.")
    {
    }
}