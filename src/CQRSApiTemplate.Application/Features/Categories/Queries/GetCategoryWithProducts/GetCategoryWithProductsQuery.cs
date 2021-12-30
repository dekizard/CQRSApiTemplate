using CQRSApiTemplate.Application.Interfaces;
using CQRSApiTemplate.Application.Features.Products.Queries.GetProductsByCatetoryIdQuery;
using CQRSApiTemplate.Resources;
using MediatR;
using Microsoft.Extensions.Logging;
using ResultModel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApiTemplate.Application.Features.Categories.Queries.GetCategoryWithProducts
{
    public class GetCategoryWithProductsQuery: IRequest<Result<CategoryDto>>
    {
        public long CategoryId { get; init; }
       
        public class GetCategoriesQueryHandler : IRequestHandler<GetCategoryWithProductsQuery, Result<CategoryDto>>
        {
            private readonly IApplicationReadDbFacade _readDbFacade;
            private readonly ILogger<GetCategoriesQueryHandler> _logger;

            public GetCategoriesQueryHandler(IApplicationReadDbFacade readDbFacade, ILogger<GetCategoriesQueryHandler> logger)
            {
                _readDbFacade = readDbFacade;
                _logger = logger;
            }

            public async Task<Result<CategoryDto>> Handle(GetCategoryWithProductsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var sql =
                        @"SELECT
                            cat.Id,
                            cat.Name, 
                            cat.Description, 
                            pro.Id,
                            pro.Name, 
                            pro.Description, 
                            pro.Price, 
                            pro.Currency 
                        FROM 
                            Category cat WITH(NOLOCK) 
                        LEFT JOIN 
                            Product pro WITH(NOLOCK) 
                        ON cat.Id = pro.CategoryId
                        WHERE cat.Id = @CategoryId";

                    var categoryDict = new Dictionary<long, CategoryDto>();

                    var categories = await _readDbFacade.QueryAsync<CategoryDto, ProductDto, CategoryDto>(sql, (category, product) =>
                    {
                        if (!categoryDict.TryGetValue(category.Id, out var currentCategory))
                        {
                            currentCategory = category;
                            categoryDict.Add(category.Id, currentCategory);
                        }

                        currentCategory.Products.Add(product);
                        return currentCategory;
                    }, new { request.CategoryId });

                    return Result<CategoryDto>.CreateSuccess(categories.Count > 0 ? categories[0] : null);
                }
                catch (Exception ex)
                {
                    _logger.LogError(new EventId(), ex, "GetCategoriesQueryHandler - error - {@GetCategoryWithProductsQuery}", request);
                    return Result<CategoryDto>.CreateFailed(SharedMessages.GetCategoryWithProducts);
                }
            }
        }
    }
}
