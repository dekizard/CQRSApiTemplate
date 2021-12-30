namespace CQRSApiTemplate.Application.Features.Products.Queries.GetProductsByCatetoryIdQuery
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
