using AutoMapper;
using GameLoanManager.Domain.Commands.Friends.CreateFriend;
using GameLoanManager.Domain.Commands.Games.CreateGame;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Test.Mocks.Entities;
using NSubstitute;

namespace GameLoanManager.Domain.Test.Mocks
{
    public static class AutoMapperMock
    {
        public static IMapper GetDefaultInstance()
        {
            return Substitute.For<IMapper>()
                .CreateGameCommandMapper()
                .CreateFriendCommandMapper();
        }
        public static IMapper CreateGameCommandMapper(this IMapper mapper)
        {
            mapper
               .Map<Game>(Arg.Any<CreateGameCommand>())
               .Returns(GameMock.GetDefaultValidInstance());

            return mapper;
        }
        public static IMapper CreateFriendCommandMapper(this IMapper mapper)
        {
            mapper
               .Map<Friend>(Arg.Any<CreateFriendCommand>())
               .Returns(FriendMock.GetDefaultValidInstance());

            return mapper;
        }
    }
}
