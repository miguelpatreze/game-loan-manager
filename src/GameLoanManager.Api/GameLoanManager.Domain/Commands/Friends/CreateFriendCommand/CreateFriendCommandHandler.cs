using AutoMapper;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Commands.Friends.CreateFriendCommand
{
    public class CreateFriendCommandHandler :
        IRequestHandler<CreateFriendCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Friend> _repository;
        private readonly ILogger<CreateFriendCommandHandler> _logger;

        public CreateFriendCommandHandler(IMapper mapper,
            IBaseRepository<Friend> repository,
            ILogger<CreateFriendCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<string> Handle(CreateFriendCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateFriendCommandHandler was called Request.Name: {request.Name}");

            var friend = _mapper.Map<Friend>(request);

            //TODO: Create validation of duplicated name

            await _repository.InsertOneAsync(friend, cancellationToken);

            _logger.LogInformation("CreateFriendCommandHandler end of execution.");
            
            return friend.Id;
        }
    }
}
