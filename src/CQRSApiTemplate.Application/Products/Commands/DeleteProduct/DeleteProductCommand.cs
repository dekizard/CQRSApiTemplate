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

namespace CQRSApiTemplate.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Result>
    {
        public long CategoryId { get; init; }
        public long Id { get; init; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteProductCommand, Result>
        {
            private readonly ICQRSApiTemplateDbContext _dbContext;
            private readonly ILogger<DeleteCategoryCommandHandler> _logger;

            public DeleteCategoryCommandHandler(ICQRSApiTemplateDbContext context, ILogger<DeleteCategoryCommandHandler> logger)
            {
                _dbContext = context;
                _logger = logger;
            }

            public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _dbContext.Categories
                            .Include(t => t.Products)
                            .SingleOrDefaultAsync(t => t.Id == request.CategoryId && t.Products.Any(p => p.Id == request.Id));

                    Guard.Against.Null(category, nameof(category), SharedMessages.vldProductMissing);

                    category.RemoveProduct(request.Id);

                    await _dbContext.SaveChangesAsync();

                    return Result.CreateSuccess();
                }
                catch (Exception ex)
                {
                    _logger.LogError(new EventId(), ex, "DeleteCategoryCommandHandler - error - {@DeleteCategoryCommand}", request);
                    return Result.CreateFailed(SharedMessages.errDeleteCategory);
                }
            }
        }
    }
}
