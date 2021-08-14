using Ardalis.GuardClauses;
using CQRSApiTemplate.Application.Interfaces;
using CQRSApiTemplate.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResultModel;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace CQRSApiTemplate.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Result>
    {
        public long CategoryId { get; init; }
        public long Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateProductCommand, Result>
        {
            private readonly ICQRSApiTemplateDbContext _dbContext;
            private readonly ILogger<UpdateCategoryCommandHandler> _logger;

            public UpdateCategoryCommandHandler(ICQRSApiTemplateDbContext context, ILogger<UpdateCategoryCommandHandler> logger)
            {
                _dbContext = context;
                _logger = logger;
            }

            public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _dbContext.Categories
                               .Include(t => t.Products)
                              .SingleOrDefaultAsync(t => t.Id == request.CategoryId && t.Products.Any(p => p.Id == request.Id));

                    Guard.Against.Null(category, nameof(category), SharedMessages.vldProductMissing);

                    category.UpdateProduct(request.Id, request.Name, request.Description, request.Price);

                    await _dbContext.SaveChangesAsync();

                    return Result.CreateSuccess();
                }
                catch (Exception ex)
                {
                    _logger.LogError(new EventId(), ex, "UpdateCategoryCommandHandler - error - {@UpdateCategoryCommand}", request);
                    return Result.CreateFailed(SharedMessages.errUpdateCategory);
                }
            }
        }
    }
}
