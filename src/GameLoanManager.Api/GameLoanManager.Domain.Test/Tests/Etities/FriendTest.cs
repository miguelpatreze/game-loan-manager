using GameLoanManager.Domain.Entities;
using System;
using Xunit;

namespace GameLoanManager.Domain.Test.Tests.Etities
{
    public class FriendTest
    {
        [Fact(DisplayName = "Should Be Valid When Name Is Informed")]
        public void ShouldBeValidWhenNameIsInformed()
        {
            var friend = new Friend("Miguel Patreze", "16997495191");

            Assert.True(friend != null);
        }
        [Fact(DisplayName = "Should throw Argument Execption When Name Is Not Informed")]
        public void ShouldThrowArgumentExceptionWhenNameIsNotInformed()
        {
            Assert.Throws<ArgumentException>(() => new Friend("", "16997495191"));
        }
    }
}
