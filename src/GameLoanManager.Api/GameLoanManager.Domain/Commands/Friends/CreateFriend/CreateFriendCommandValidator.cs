using FluentValidation;

namespace GameLoanManager.Domain.Commands.Friends.CreateFriend
{
    public class CreateFriendCommandValidator : AbstractValidator<CreateFriendCommand>
    {
        public CreateFriendCommandValidator()
        {
            RuleFor(friend => friend.Name)
                .NotEmpty()
                .WithMessage("Obrigatório informar o Nome do Amigo a ser inserido.")
                .MaximumLength(100)
                .WithMessage("O nome do amigo não pode ultrapassar 100 caracteres");

            RuleFor(friend => friend.CellPhoneNumber)
                .Length(11)
                .WithMessage("O Número do Celular deve conter exatamente 11 caracteres.")
                .NotEmpty()
                .WithMessage("Obrigatório informar o Número do Celular do Amigo a ser inserido.");
        }
    }
}
