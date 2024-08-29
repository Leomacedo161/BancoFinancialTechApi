using FluentValidation;
using FullTechApiDesafio.Models;

namespace FullTechApiDesafio.Validations;

public class ContaValidator : AbstractValidator<Conta>
{
    public ContaValidator()
    {
        RuleFor(x => x.NumeroConta).NotEmpty().Length(5, 10);
        RuleFor(x => x.Saldo).GreaterThanOrEqualTo(0);
    }
}
