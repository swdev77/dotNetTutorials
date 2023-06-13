using Domain.Products;
using Microsoft.EntityFrameworkCore;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; set; }
}