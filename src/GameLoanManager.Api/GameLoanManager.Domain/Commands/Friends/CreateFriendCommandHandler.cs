using AutoMapper;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Commands.Friends
{
    public class CreateFriendCommandHandler :
        IRequestHandler<CreateFriendCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Friend> _repository;

        public CreateFriendCommandHandler(IMapper mapper,
            IBaseRepository<Friend> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateFriendCommand request, CancellationToken cancellationToken)
        {
            var friend = _mapper.Map<Friend>(request);

            //TODO: Create validation of duplicated name

            await _repository.InsertOneAsync(friend, cancellationToken);

            return await Unit.Task;
        }
    }
}
