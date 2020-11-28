using FluentValidation;

namespace GameLoanManager.Domain.Commands.Friends.PatchFriendCommand
{
    public class PatchFriendCommandValidator : AbstractValidator<PatchFriendCommand>
    {
        public PatchFriendCommandValidator()
        {
            RuleFor(friend => friend.Id)
                .NotEmpty()
                .WithMessage("Obrigatório informar o Id do Amigo a ser alterado.");

            RuleFor(friend => friend.CellPhoneNumber)
                .MaximumLength(11)
                .WithMessage("O Número do Celular não pode exceder 11 caracteres.")
                .When(friend => !string.IsNullOrWhiteSpace(friend.CellPhoneNumber));
        }

    }
}
