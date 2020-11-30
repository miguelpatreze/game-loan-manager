using GameLoanManager.Domain.Entities;
using System;
using Xunit;

namespace GameLoanManager.Domain.Test.Tests.Etities
{
    public class GameTest
    {
        [Fact(DisplayName = "Should Be Valid When Name Is Informed")]
        public void ShouldBeValidWhenNameIsInformed()
        {
            var game = new Game("Dark Souls 3");

            Assert.True(game != null);
        }
        [Fact(DisplayName = "Should throw Argument Execption When Name Is Not Informed")]
        public void ShouldThrowArgumentExceptionWhenNameIsNotInformed()
        {
            Assert.Throws<ArgumentException>(() => new Game(""));
        }
    }
}
