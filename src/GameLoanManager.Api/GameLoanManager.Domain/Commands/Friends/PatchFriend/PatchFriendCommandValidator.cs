using FluentValidation;

namespace GameLoanManager.Domain.Commands.Friends.PatchFriend
{
    public class PatchFriendCommandValidator : AbstractValidator<PatchFriendCommand>
    {
        public PatchFriendCommandValidator()
        {
            RuleFor(friend => friend.Id)
                .NotEmpty()
                .WithMessage("Obrigatório informar o Id do Amigo a ser alterado.");

            RuleFor(friend => friend.CellPhoneNumber)
                .Length(11)
                .WithMessage("O Número do Celular deve conter exatamente 11 caracteres.")
                .When(friend => !string.IsNullOrWhiteSpace(friend.CellPhoneNumber));
        }

    }
}
