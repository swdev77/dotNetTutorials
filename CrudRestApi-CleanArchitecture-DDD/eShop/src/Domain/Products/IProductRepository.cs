namespace Domain.Products;
public interface IProductRepository
{
    void Add(Product product);
    Task<Product?> GetByIdAsync(ProductId productId);
    void Update(Product product);
    void Remove(Product product);
}