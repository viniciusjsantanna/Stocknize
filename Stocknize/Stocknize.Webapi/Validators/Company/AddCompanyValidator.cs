using FluentValidation;
using Stocknize.Domain.Models.Company;

namespace Stocknize.Webapi.Validators.Company
{
    public class AddCompanyValidator : AbstractValidator<CompanyInputModel>
    {
        public AddCompanyValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("O Nome da Empresa é obrigatório!");
        }
    }
}
