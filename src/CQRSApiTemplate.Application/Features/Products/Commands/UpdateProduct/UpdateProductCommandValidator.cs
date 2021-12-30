using CQRSApiTemplate.Resources;
using FluentValidation;

namespace CQRSApiTemplate.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(t => t.Price).ExclusiveBetween(0, 1_000_000_000)
                .WithMessage(SharedMessages.vldNonPositivePrice);
        }
    }
}
