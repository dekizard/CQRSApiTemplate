using Ardalis.GuardClauses;
using CQRSApiTemplate.Domain.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace CQRSApiTemplate.Domain.Entities
{
    public class Category : AuditableEntity, IAggreagateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private List<Product> _products = new();
        public IReadOnlyList<Product> Products => _products.AsReadOnly();

        private Category() { }

        public Category(string name, string description)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));

            Name = name;
            Description = description;
        }

        public void UpdateCategory(string name, string description)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));

            Name = name;
            Description = description;
        }

        public void AddProduct(string name, string description, decimal price, string currency)
        {
            _products.Add(new Product(name, description, price, currency));
        }

        public void UpdateProduct(long id, string name, string description, decimal price)
        {
            var product = _products.SingleOrDefault(t => t.Id == id);
            product.UpdateProduct(name, description, price);
        }

        public void RemoveProduct(long id)
        {
            _products.Remove(_products.FirstOrDefault(t => t.Id == id));
        }
    }
}
