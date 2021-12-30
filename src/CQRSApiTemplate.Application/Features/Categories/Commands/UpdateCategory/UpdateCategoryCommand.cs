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

namespace CQRSApiTemplate.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Result>
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }

        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
        {
            private readonly ICQRSApiTemplateDbContext _dbContext;
            private readonly ILogger<UpdateCategoryCommandHandler> _logger;

            public UpdateCategoryCommandHandler(ICQRSApiTemplateDbContext context, ILogger<UpdateCategoryCommandHandler> logger)
            {
                _dbContext = context;
                _logger = logger;
            }

            public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _dbContext.Categories
                            .SingleOrDefaultAsync(t => t.Id == request.Id);

                    Guard.Against.Null(category, nameof(category), SharedMessages.vldCategoryMissing);

                    category.Update(request.Name, request.Description);

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
