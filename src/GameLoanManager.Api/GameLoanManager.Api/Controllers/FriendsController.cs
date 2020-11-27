using GameLoanManager.Domain.Commands.Friends;
using GameLoanManager.Domain.Queries.Friends.GetFriendById;
using GameLoanManager.Domain.Queries.Friends.GetFriends;
using GameLoanManager.Domain.Reponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace GameLoanManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FriendsController : BaseController<FriendsController>
    {
        public FriendsController(ILogger<FriendsController> logger, IMediator mediator) : base(logger, mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<Response>> Get()
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
