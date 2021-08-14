using Ardalis.GuardClauses;
using CQRSApiTemplate.Application.Interfaces;
using CQRSApiTemplate.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResultModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApiTemplate.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Result>
    {
        public long Id { get; init; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
        {
            private readonly ICQRSApiTemplateDbContext _dbContext;
            private readonly ILogger<DeleteCategoryCommandHandler> _logger;

            public DeleteCategoryCommandHandler(ICQRSApiTemplateDbContext context, ILogger<DeleteCategoryCommandHandler> logger)
            {
                _dbContext = context;
                _logger = logger;
            }

            public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _dbContext.Categories
                            .SingleOrDefaultAsync(t => t.Id == request.Id);
                    
                    Guard.Against.Null(category, nameof(category), SharedMessages.vldCategoryMissing);

                    _dbContext.Categories.Remove(category);
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
