using AutoMapper;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Contracts;
using GameLoanManager.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GameLoanManager.Domain.Commands.Friends.PatchFriendCommand
{
    public class PatchFriendCommandHandler :
        IRequestHandler<PatchFriendCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Friend> _repository;
        private readonly ILogger<PatchFriendCommandHandler> _logger;
        private readonly INotificationContext _notificationContext;

        public PatchFriendCommandHandler(IMapper mapper,
            IBaseRepository<Friend> repository,
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
            _logger.LogInformation($"CreateFriendCommandHandler was called Request.Name: {request.Name}");

            var friend = await _repository.GetByIdAsync(request.Id);

            if (friend == null)
            {
                _notificationContext.AddNotification("Friend not Found", $"The Friend with id:{request.Id} was not found.");
                return await Unit.Task;
            }

            //TODO: Create validation of duplicated name

            if (!string.IsNullOrWhiteSpace(request.Name) && !friend.Name.Equals(request.Name, System.StringComparison.OrdinalIgnoreCase))
                friend.SetName(request.Name);

            if (!string.IsNullOrWhiteSpace(request.CellPhoneNumber) && !friend.CellPhoneNumber.Equals(request.CellPhoneNumber, System.StringComparison.OrdinalIgnoreCase))
                friend.SetCellPhoneNumber(request.CellPhoneNumber);

            await _repository.ReplaceOneAsync(friend, cancellationToken);

            return await Unit.Task;
        }

    }
}
