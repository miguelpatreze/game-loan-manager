using FluentValidation;

namespace GameLoanManager.Domain.Commands.Games.CreateGame
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(Game => Game.Name)
                .NotEmpty()
                .WithMessage("Obrigatório informar o Nome do Jogo a ser inserido.")
                .MaximumLength(50)
                .WithMessage("O nome do jogo não pode ultrapassar 50 caracteres.");

        }
    }
}
