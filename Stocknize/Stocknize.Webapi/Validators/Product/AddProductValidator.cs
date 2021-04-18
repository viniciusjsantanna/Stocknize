using FluentValidation;
using Stocknize.Domain.Models.Products;

namespace Stocknize.Webapi.Validators.Product
{
    public class AddProductValidator : AbstractValidator<ProductInputModel>
    {
        public AddProductValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("O Nome do Produto é Obrigatório!")
                .MinimumLength(3)
                .WithMessage("O Nome do Produto deve conter pelo menos 3 caracteres");

            RuleFor(e => e.Price)
                .GreaterThan(0M)
                .WithMessage("O Preço deve ser maior que zero");
        }
    }
}
