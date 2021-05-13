using FluentValidation;
using Stocknize.Domain.Models.User;

namespace Stocknize.Webapi.Validators.User
{
    public class AddUserValidator : AbstractValidator<UserInputModel>
    {
        public AddUserValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("O Nome é obrigatório!");

            RuleFor(e => e.Cpf)
                .NotEmpty()
                .WithMessage("O Cpf é obrigatório!")
                .MinimumLength(11)
                .WithMessage("O Cpf deve ter no minimo 11 caracteres")
                .MaximumLength(11)
                .WithMessage("O Cpf deve ter no máximo 11 caracteres");
        }
    }
}
