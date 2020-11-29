using AutoMapper;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Commands.Games.CreateGame
{
    public class CreateGameCommandHandler :
        IRequestHandler<CreateGameCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Game> _repository;
        private readonly ILogger<CreateGameCommandHandler> _logger;

        public CreateGameCommandHandler(IMapper mapper,
            IBaseRepository<Game> repository,
            ILogger<CreateGameCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<string> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateGameCommandHandler was called Request.Name: {request.Name}");

            var game = _mapper.Map<Game>(request);

            //TODO: Create validation of duplicated name

            await _repository.InsertOneAsync(game, cancellationToken);

            _logger.LogInformation("CreateGameCommandHandler end of execution");
            
            return game.Id;
        }
    }
}
