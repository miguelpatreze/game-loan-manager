using FluentAssertions;
using GameLoanManager.Domain.Commands.Friends.PatchFriend;
using GameLoanManager.Domain.Test.Mocks.Commands.PatchFriend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameLoanManager.Domain.Test.Tests.Commands.PatchFriend
{
    public class PatchFriendCommandValidatorTest
    {

        private readonly PatchFriendCommandValidator _validator = new PatchFriendCommandValidator();

        [Fact(DisplayName = "Should Be Valid When Command Is Populated")]
        public void ShouldBeValidWhenCommandIsPopulated()
        {
            var validate = _validator.Validate(PatchFriendCommandMock.GetDefaultValidInstance());
            validate.IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Should Not Be Valid When Command Is Populated And CellPhoneNumber Is Ten Characteres Length")]
        public void ShouldNotBeValidWhenCommandIsPopulatedAndCellPhoneNumberIsTenCharacteresLength()
        {
            var validate = _validator.Validate(PatchFriendCommandMock.GetCellPhoneNumberLessThenElevenCharacteresLengthInstance());
            validate.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Should Not Be Valid When Command Is Populated And CellPhoneNumber Is Twelve Characteres Length")]
        public void ShouldNotBeValidWhenCommandIsPopulatedAndCellPhoneNumberIsTwelveCharacteresLength()
        {
            var validate = _validator.Validate(PatchFriendCommandMock.GetCellPhoneNumberMoreThenElevenCharacteresLengthInstance());
            validate.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Should Not Be Valid When Command Is Populated And Name Is More Then Hundred Characteres Length")]
        public void ShouldNotBeValidWhenCommandIsPopulatedAndNameIsMoreThenHundredCharacteres()
        {
            var validate = _validator.Validate(PatchFriendCommandMock.GetNameMoreThenHundredCharacteresLengthInstance());
            validate.IsValid.Should().BeFalse();
        }
    }
}
