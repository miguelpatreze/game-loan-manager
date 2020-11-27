using GameLoanManager.Domain.Notifications;
using GameLoanManager.Domain.Reponses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameLoanManager.Api
{
    [Produces("application/json")]
    [ApiController]
    public class BaseController<T> : Controller
    {
        protected readonly IMediator _mediator;
        protected readonly ILogger<T> _logger;

        protected BaseController(ILogger<T> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        protected async Task<ActionResult<Response>> CreateResponse(IRequest<Response> request, HttpStatusCode successCode)
        {
            var payload = await _mediator.Send(request).ConfigureAwait(false);
            var type = payload.Notifications?.FirstOrDefault()?.Type ?? NotificationType.Success;

            switch (type)
            {
                case NotificationType.Error:
                    return StatusCode((int)HttpStatusCode.BadRequest, payload);
                case NotificationType.NotFound:
                    return StatusCode((int)HttpStatusCode.NotFound, payload);
                default:
                    if (HttpContext.Request.Method == HttpMethod.Get.Method && payload is null)
                        return StatusCode((int)HttpStatusCode.NotFound, payload);

                    return StatusCode((int)successCode, payload);
            }
        }


    }
}
