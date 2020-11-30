using FluentAssertions;
using GameLoanManager.Domain.Commands.Friends.CreateFriend;
using GameLoanManager.Domain.Test.Mocks.Commands.CreateFriend;
using Xunit;

namespace GameLoanManager.Domain.Test.Tests.Commands.CreateFriend
{
    public class CreateFriendCommandValidatorTest
    {
        private readonly CreateFriendCommandValidator _validator = new CreateFriendCommandValidator();

        [Fact(DisplayName = "Should Be Valid When Command Is Populated")]
        public void ShouldBeValidWhenCommandIsPopulated()
        {
            var validate = _validator.Validate(CreateFriendCommandMock.GetDefaultValidInstance());
            validate.IsValid.Should().BeTrue();
        }

        [Fact(DisplayName = "Should Not Be Valid When Command Is Populated And Name Is Empty")]
        public void ShouldNotBeValidWhenCommandIsPopulatedAndNameIsEmpty()
        {
            var validate = _validator.Validate(CreateFriendCommandMock.GetEmptyNameInstance());
            validate.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Should Not Be Valid When Command Is Populated And CellPhoneNumber Is Empty")]
        public void ShouldNotBeValidWhenCommandIsPopulatedAndCellPhoneNumberIsEmpty()
        {
            var validate = _validator.Validate(CreateFriendCommandMock.GetEmptyCellPhoneNumberInstance());
            validate.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Should Not Be Valid When Command Is Populated And CellPhoneNumber Is Less Then Elevn Characteres Length")]
        public void ShouldNotBeValidWhenCommandIsPopulatedAndCellPhoneNumberIsLessThenElevenCharacteresLength()
        {
            var validate = _validator.Validate(CreateFriendCommandMock.GetCellPhoneNumberLeeThenElevenCharacteresLengthInstance());
            validate.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Should Not Be Valid When Command Is Populated And CellPhoneNumber Is More Then Eleven Characteres Length")]
        public void ShouldNotBeValidWhenCommandIsPopulatedAndCellPhoneNumberIsMoreThenElevenCharacteresLength()
        {
            var validate = _validator.Validate(CreateFriendCommandMock.GetCellPhoneNumberMoreThenElevenCharacteresLengthInstance());
            validate.IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "Should Not Be Valid When Command Is Populated And Name Is More Then Hundred Characteres Length")]
        public void ShouldNotBeValidWhenCommandIsPopulatedAndNameIsMoreThenHundredCharacteres()
        {
            var validate = _validator.Validate(CreateFriendCommandMock.GetNameMoreThenHundredCharacteresLengthInstance());
            validate.IsValid.Should().BeFalse();
        }
    }
}
