using FluentValidation;

namespace BancoChu.Entidades.Dto
{
    public class MovimentacaoDtoValidator : AbstractValidator<MovimentacaoDto>
    {
        public MovimentacaoDtoValidator()
        {
            RuleFor(m => m.CpfCnpj)
                .NotEmpty().WithMessage("O campo CpfCnpj é obrigatório.");

            RuleFor(m => m.Valor)
                .NotEmpty().WithMessage("O campo Valor é obrigatório.")
                .GreaterThan(0).WithMessage("O Valor deve ser maior que zero.");

            RuleFor(m => m.Tipo)
                .IsInEnum().WithMessage("Tipo deve ser um valor válido.");

            RuleFor(m => m)
                .Must(m => !string.IsNullOrEmpty(m.ChavePix) || (m.IdContaDestino > 0 && m.AgenciaDestino > 0))
                .WithMessage("Não existe um destinatário identificado.");
        }
    }
}