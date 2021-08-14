using CQRSApiTemplate.Domain.Abstraction;
using CQRSApiTemplate.Domain.Enums;
using System;
using System.Collections.Generic;

namespace CQRSApiTemplate.Domain.ValueObjects
{
    public class Money: ValueObject
    {
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        internal Money(decimal amount, Currency currency)
        {            
            Amount = amount;
            Currency = currency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Currency;
            yield return Amount;
        }
    }
}
