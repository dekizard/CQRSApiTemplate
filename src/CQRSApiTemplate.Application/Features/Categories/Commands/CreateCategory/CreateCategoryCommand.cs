using CQRSApiTemplate.Application.Common.ResultModel;
using CQRSApiTemplate.Application.Interfaces;
using CQRSApiTemplate.Domain.Entities;
using CQRSApiTemplate.Resources;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApiTemplate.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Result>
    {
        public string Name { get; init; }
        public string Description { get; init; }

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result>
        {
            private readonly ICQRSApiTemplateDbContext _dbContext;
            private readonly ILogger<CreateCategoryCommandHandler> _logger;

            public CreateCategoryCommandHandler(ICQRSApiTemplateDbContext context, ILogger<CreateCategoryCommandHandler> logger)
            { 

                _dbContext = context;
                _logger = logger;
            }

            public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var newCategory = new Category(request.Name, request.Description);
                    await _dbContext.Categories.AddAsync(newCategory);

                    await _dbContext.SaveChangesAsync();

                    return Result.CreateSuccess();
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(new EventId(), ex, "CreateCategoryCommandHandler - error - {@CreateCategoryCommand}", request);
                    return Result.CreateFailed(SharedMessages.errCreateCategory);
                }
            }
        }
    }
}
