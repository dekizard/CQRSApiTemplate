using CQRSApiTemplate.Application.Features.Products.Queries.GetProductsByCatetoryIdQuery;

namespace CQRSApiTemplate.Application.Features.Categories.Queries.GetCategoryWithProducts;

public class CategoryDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IList<ProductDto> Products { get; set; } = new List<ProductDto>();
}
