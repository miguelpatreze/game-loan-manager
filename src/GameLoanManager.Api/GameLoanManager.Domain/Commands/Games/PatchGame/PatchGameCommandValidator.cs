using FluentValidation;

namespace GameLoanManager.Domain.Commands.Games.PatchGame
{
    public class PatchGameCommandValidator : AbstractValidator<PatchGameCommand>
    {
        public PatchGameCommandValidator()
        {
            RuleFor(Game => Game.Id)
                .NotEmpty()
                .WithMessage("Obrigatório informar o Id do Jogo a ser alterado.");
        }

    }
}
