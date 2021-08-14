using Ardalis.GuardClauses;
using CQRSApiTemplate.Domain.Abstraction;
using CQRSApiTemplate.Domain.Enums;
using CQRSApiTemplate.Domain.ValueObjects;
using System;

namespace CQRSApiTemplate.Domain.Entities
{
    public class Product: AuditableEntity
    {
        public long CategoryId { get; private set; }
        public Category Category { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }

        private Product() { }

        internal Product(string name, string description, decimal price, string currency)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NullOrEmpty(currency, nameof(currency));

            Name = name;
            Description = description;
            Price = new Money(price, (Currency)Enum.Parse(typeof(Currency), currency, true));
        }

        internal void UpdateProduct(string name, string description, decimal price)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));

            Name = name;
            Description = description;
            Price = new Money(price, Price.Currency);
        }
    }
}