using CQRSApiTemplate.Application.Interfaces;
using CQRSApiTemplate.Resources;
using MediatR;
using Microsoft.Extensions.Logging;
using ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApiTemplate.Application.Features.Products.Queries.GetProductsByCatetoryIdQuery
{
    public class GetProductsByCatetoryIdQuery : IRequest<Result<IList<ProductDto>>>
    {
        public long CategoryId { get; init; }

        public class GetCategoriesQueryHandler : IRequestHandler<GetProductsByCatetoryIdQuery, Result<IList<ProductDto>>>
        {
            private readonly IApplicationReadDbFacade _readDbFacade;
            private readonly ILogger<GetCategoriesQueryHandler> _logger;

            public GetCategoriesQueryHandler(IApplicationReadDbFacade readDbFacade, ILogger<GetCategoriesQueryHandler> logger)
            {
                _readDbFacade = readDbFacade;
                _logger = logger;
            }

            public async Task<Result<IList<ProductDto>>> Handle(GetProductsByCatetoryIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var products = (await _readDbFacade.QueryAsync<ProductDto>(
                            @"SELECT
                                Id,
                                Name,
                                Description,
                                Price,
                                Currency
                            FROM
                                Product pro WITH(NOLOCK)
                            WHERE pro.CategoryId = @CategoryId", new { request.CategoryId })).ToList();

                    return Result<IList<ProductDto>>.CreateSuccess(products);
                }
                catch (Exception ex)
                {
                    _logger.LogError(new EventId(), ex, "GetCategoriesQueryHandler - error - {@GetProductsByCatetoryIdQuery}", request);
                    return Result<IList<ProductDto>>.CreateFailed(SharedMessages.GetProductsByCategoryId);
                }
            }
        }
    }
}
