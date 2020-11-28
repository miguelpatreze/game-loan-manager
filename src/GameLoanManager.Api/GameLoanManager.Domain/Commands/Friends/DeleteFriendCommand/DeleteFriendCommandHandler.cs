using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Commands.Friends.DeleteFriendCommand
{
    public class DeleteFriendCommandHandler :
        IRequestHandler<DeleteFriendCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Friend> _repository;
        private readonly ILogger<DeleteFriendCommandHandler> _logger;
        private readonly INotificationContext _notificationContext;

        public DeleteFriendCommandHandler(IMapper mapper,
            IBaseRepository<Friend> repository,
            ILogger<DeleteFriendCommandHandler> logger,
            INotificationContext notificationContext)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        public async Task<Unit> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"DeleteFriendCommandHandler was called Request.Id: {request.Id}");

            var friend = await _repository.GetByIdAsync(request.Id);

            if (friend == null)
            {
                _notificationContext.AddNotification("Amigo não encontrado", $"O amigo com o id:{request.Id} não foi encontrado.");
                return await Unit.Task;
            }

            await _repository.DeleteOneAsync(friend, cancellationToken);

            return await Unit.Task;
        }
    }
}
