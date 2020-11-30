using GameLoanManager.Api.Responses;
using GameLoanManager.CrossCutting.Notification;
using GameLoanManager.Domain.Commands.Games;
using GameLoanManager.Domain.Commands.Games.CreateGame;
using GameLoanManager.Domain.Commands.Games.LoanGame;
using GameLoanManager.Domain.Commands.Games.DeleteGame;
using GameLoanManager.Domain.Commands.Games.PatchGame;
using GameLoanManager.Domain.Queries.Games.GetGameById;
using GameLoanManager.Domain.Queries.Games.GetGames;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using GameLoanManager.Domain.Commands.Games.ReturnGame;

namespace GameLoanManager.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    [Route("[controller]")]
    public class GamesController : BaseController<GamesController>
    {
        public GamesController(ILogger<GamesController> logger, IMediator mediator, INotificationContext notificationContext)
            : base(logger, mediator, notificationContext)
        {
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return await CreateResponse(new GetGamesQuery(), HttpStatusCode.Created);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> Get(string id)
        {
            return await CreateResponse(new GetGameByIdQuery(id), HttpStatusCode.Created);
        }
        [HttpPost]
        public async Task<ActionResult<Response>> Post([FromBody] CreateGameCommand command)
        {
            return await CreateResponse(command, HttpStatusCode.Created);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Response>> Patch(string id, [FromBody] PatchGameCommand command)
        {
            command.SetId(id);
            return await CreateResponse(command, HttpStatusCode.OK);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(string id)
        {
            return await CreateResponse(new DeleteGameCommand(id), HttpStatusCode.OK);
        }

        [HttpPost("{id}/Loans/{friendId}")]
        public async Task<ActionResult<Response>> PostLoan(string id, string friendId)
        {
            return await CreateResponse(new LoanGameCommand(id, friendId), HttpStatusCode.OK);
        }

        [HttpPost("{id}/Devolutions")]
        public async Task<ActionResult<Response>> PostDevolution(string id)
        {
            return await CreateResponse(new ReturnGameCommand(id), HttpStatusCode.OK);
        }
    }
}
