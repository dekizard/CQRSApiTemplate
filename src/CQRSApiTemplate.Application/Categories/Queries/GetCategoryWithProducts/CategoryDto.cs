using System.Collections.Generic;
using CQRSApiTemplate.Application.Products.Queries.GetProductsByCatetoryIdQuery;

namespace CQRSApiTemplate.Application.Categories.Queries.GetCategoryWithProducts
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
