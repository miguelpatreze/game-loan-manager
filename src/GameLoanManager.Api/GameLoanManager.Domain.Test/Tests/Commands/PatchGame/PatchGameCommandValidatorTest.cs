using FluentAssertions;
using GameLoanManager.Domain.Commands.Games.PatchGame;
using GameLoanManager.Domain.Test.Mocks.Commands.PatchGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameLoanManager.Domain.Test.Tests.Commands.PatchGame
{
    public class PatchGameCommandValidatorTest
    {


        private readonly PatchGameCommandValidator _validator = new PatchGameCommandValidator();

        [Fact(DisplayName = "Should Be Valid When Command Is Populated")]
        public void ShouldBeValidWhenCommandIsPopulated()
        {
            var validate = _validator.Validate(PatchGameCommandMock.GetDefaultValidInstance());
            validate.IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Should Not Be Valid When Command Is Populated And Id Is Empty")]
        public void ShouldNotBeValidWhenCommandIsPopulatedAndNameIsEmpty()
        {
            var validate = _validator.Validate(PatchGameCommandMock.GetEmptyIdInstance());
            validate.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Should Not Be Valid When Command Is Populated And Name Is More Then Hundred Characteres Length")]
        public void ShouldNotBeValidWhenCommandIsPopulatedAndNameIsMoreThenHundredCharacteres()
        {
            var validate = _validator.Validate(PatchGameCommandMock.GetNameMoreThenHundredCharacteresLengthInstance());
            validate.IsValid.Should().BeFalse();
        }
    }
}
