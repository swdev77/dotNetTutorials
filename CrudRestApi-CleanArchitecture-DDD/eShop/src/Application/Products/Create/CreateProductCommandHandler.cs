using MediatR;
using Domain.Products;
using Application.Data;

namespace Application.Products.Create;

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            id: new ProductId(Guid.NewGuid()),
            name: request.Name,
            price: new Money(request.Currency, request.Amount),
            sku: Sku.Create(request.Sku)!);


        _productRepository.Add(product); 

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
