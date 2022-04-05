using Ardalis.GuardClauses;
using CQRSApiTemplate.Application.Common.ResultModel;
using CQRSApiTemplate.Application.Interfaces;
using CQRSApiTemplate.Resources;
using Microsoft.EntityFrameworkCore;

namespace CQRSApiTemplate.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Result>
{
    public long CategoryId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public string Currency { get; init; }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly ICQRSApiTemplateDbContext _dbContext;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(ICQRSApiTemplateDbContext context, ILogger<CreateProductCommandHandler> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _dbContext.Categories
                        .SingleOrDefaultAsync(t => t.Id == request.CategoryId);

                Guard.Against.Null(category, nameof(category), SharedMessages.vldCategoryMissing);

                category.AddProduct(request.Name, request.Description, request.Price, request.Currency);
                
                await _dbContext.SaveChangesAsync();

                return Result.CreateSuccess();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(new EventId(), ex, "CreateProductCommandHandler - error - {@CreateProductCommand}", request);
                return Result.CreateFailed(SharedMessages.errCreateProduct);
            }
        }
    }
}
