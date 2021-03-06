﻿using FluentValidation;

namespace GameLoanManager.Domain.Commands.Friends.PatchFriend
{
    public class PatchFriendCommandValidator : AbstractValidator<PatchFriendCommand>
    {
        public PatchFriendCommandValidator()
        {
            RuleFor(friend => friend.Name)
                .MaximumLength(100)
                .WithMessage("O nome do amigo não pode ultrapassar 100 caracteres")
                .When(friend => !string.IsNullOrWhiteSpace(friend.Name));

            RuleFor(friend => friend.CellPhoneNumber)
                .Length(11)
                .WithMessage("O Número do Celular deve conter exatamente 11 caracteres.")
                .When(friend => !string.IsNullOrWhiteSpace(friend.CellPhoneNumber));
        }

    }
}
