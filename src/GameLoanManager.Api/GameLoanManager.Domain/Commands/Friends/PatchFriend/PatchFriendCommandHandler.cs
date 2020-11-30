using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Commands.Friends.PatchFriend
{
    public class PatchFriendCommandHandler :
        IRequestHandler<PatchFriendCommand>
    {
        private readonly IMapper _mapper;
        private readonly IFriendRepository _repository;
        private readonly ILogger<PatchFriendCommandHandler> _logger;
        private readonly INotificationContext _notificationContext;

        public PatchFriendCommandHandler(IMapper mapper,
            IFriendRepository repository,
            ILogger<PatchFriendCommandHandler> logger,
            INotificationContext notificationContext)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        public async Task<Unit> Handle(PatchFriendCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"PatchFriendCommandHandler was called Request.Id: {request.Id}");

            var friend = await _repository.GetByIdAsync(request.Id);

            if (friend == null)
            {
                _notificationContext.AddNotification("Amigo não encontrado", $"O amigo com o id:{request.Id} não foi encontrado.");
                return await Unit.Task;
            }

            if (!string.IsNullOrWhiteSpace(request.Name) && !friend.Name.Equals(request.Name, System.StringComparison.OrdinalIgnoreCase))
                friend.SetName(request.Name);

            if (!string.IsNullOrWhiteSpace(request.CellPhoneNumber) && !friend.CellPhoneNumber.Equals(request.CellPhoneNumber, System.StringComparison.OrdinalIgnoreCase))
                friend.SetCellPhoneNumber(request.CellPhoneNumber);

            await _repository.ReplaceOneAsync(friend, cancellationToken);

            _logger.LogInformation("PatchFriendCommandHandler end of execution");
            
            return await Unit.Task;
        }

    }
}
