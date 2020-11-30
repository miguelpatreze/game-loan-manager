using FluentValidation;

namespace GameLoanManager.Domain.Commands.Games.PatchGame
{
    public class PatchGameCommandValidator : AbstractValidator<PatchGameCommand>
    {
        public PatchGameCommandValidator()
        {
            RuleFor(friend => friend.Name)
                .NotEmpty()
                .WithMessage("Obrigatório informar o Nome do Jogo a ser inserido.")
                .MaximumLength(50)
                .WithMessage("O nome do jogo não pode ultrapassar 50 caracteres.")
                .When(game => !string.IsNullOrWhiteSpace(game.Name));

        }

    }
}
