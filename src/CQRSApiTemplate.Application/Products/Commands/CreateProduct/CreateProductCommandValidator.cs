using FluentValidation;
using CQRSApiTemplate.Resources;
using CQRSApiTemplate.Domain.Enums;

namespace CQRSApiTemplate.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(t => t.Price).ExclusiveBetween(0, 1_000_000_000)
                .WithMessage(SharedMessages.vldNonPositivePrice);

            RuleFor(t => t.Currency)
             .IsEnumName(typeof(Currency), caseSensitive: false)
             .WithMessage(SharedMessages.vldCurrency);
        }
    }
}
