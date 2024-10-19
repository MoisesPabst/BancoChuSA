using FluentValidation;

namespace BancoChu.Entidades.Dto
{
    public class ContaDtoValidator : AbstractValidator<ContaDto>
    {
        public ContaDtoValidator()
        {
            RuleFor(m => m.CpfCnpj)
                .NotEmpty().WithMessage("O campo CpfCnpj é obrigatório.");

            RuleFor(m => m.Nome)
                .NotEmpty().WithMessage("O campo Nome é obrigatório.");
        }
    }
}
