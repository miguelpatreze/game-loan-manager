using GameLoanManager.Api.Responses;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Commands.Friends;
using GameLoanManager.Domain.Queries.Friends.GetFriendById;
using GameLoanManager.Domain.Queries.Friends.GetFriends;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace GameLoanManager.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    [Route("[controller]")]
    public class FriendsController : BaseController<FriendsController>
    {
        public FriendsController(ILogger<FriendsController> logger, IMediator mediator, INotificationContext notificationContext)
            : base(logger, mediator, notificationContext)
        {
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return await CreateResponse(new GetFriendsQuery(), HttpStatusCode.Created);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> Get(string id)
        {
            return await CreateResponse(new GetFriendByIdQuery(id), HttpStatusCode.Created);
        }
        [HttpPost]
        public async Task<ActionResult<Response>> Post([FromBody] CreateFriendCommand command)
        {
            return await CreateResponse(command, HttpStatusCode.Created);
        }
    }
}
