using FluentValidation;

namespace GameLoanManager.Domain.Commands.Friends.CreateFriendCommand
{
    public class CreateFriendCommandValidator : AbstractValidator<CreateFriendCommand>
    {
        public CreateFriendCommandValidator()
        {
            RuleFor(friend => friend.Name)
                .NotEmpty()
                .WithMessage("Obrigatório informar o Nome do Amigo a ser inserido.");

            RuleFor(friend => friend.CellPhoneNumber)
                .MaximumLength(11)
                .WithMessage("O Número do Celular não pode exceder 11 caracteres.")
                .NotEmpty()
                .WithMessage("Obrigatório informar o Número do Celular do Amigo a ser inserido.");
        }
    }
}
