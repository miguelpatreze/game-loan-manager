using FluentAssertions;
using GameLoanManager.Domain.Commands.Games.CreateGame;
using GameLoanManager.Domain.Test.Mocks.Commands.CreateGame;
using Xunit;

namespace GameLoanManager.Domain.Test.Tests.Commands.CreateGame
{
    public class CreateGameCommandValidatorTest
    {

        private readonly CreateGameCommandValidator _validator = new CreateGameCommandValidator();

        [Fact(DisplayName = "Should Be Valid When Command Is Populated")]
        public void ShouldBeValidWhenCommandIsPopulated()
        {
            var validate = _validator.Validate(CreateGameCommandMock.GetDefaultValidInstance());
            validate.IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Should Not Be Valid When Command Is Populated And Name Is More Then Fifty Characteres Length")]
        public void ShouldNotBeValidWhenCommandIsPopulatedAndNameIsMoreThenFiftyCharacteres()
        {
            var validate = _validator.Validate(CreateGameCommandMock.GetNameMoreThenFiftyCharacteresLengthInstance());
            validate.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Should Not Be Valid When Command Is Populated And Name Is Empty")]
        public void ShouldNotBeValidWhenCommandIsPopulatedAndNameIsEmpty()
        {
            var validate = _validator.Validate(CreateGameCommandMock.GetEmptyNameInstance());
            validate.IsValid.Should().BeFalse();
        }
    }
}
