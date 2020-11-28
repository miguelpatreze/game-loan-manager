using GameLoanManager.Api.Responses;
using GameLoanManager.CrossCutting;
using GameLoanManager.CrossCutting.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly INotificationContext _notificationContext;

        protected BaseController(ILogger<T> logger, IMediator mediator, INotificationContext notificationContext)
        {
            _mediator = mediator;
            _logger = logger;
            _notificationContext = notificationContext;
        }

        protected async Task<ActionResult> CreateResponse<TRequest>(TRequest request, HttpStatusCode successCode) where TRequest : IBaseRequest
        {
            try
            {
                var data = await _mediator.Send(request).ConfigureAwait(false);
                var method = HttpContext.Request.Method;

                if (_notificationContext.HasNotifications)
                {
                    var response = new Response(notifications: _notificationContext.Notifications.Select(x => new { x.Key, x.ErrorMessage }));

                    if (method == HttpMethod.Get.Method && (data is null || data == default))
                        return NotFound(response);

                    return BadRequest(response);
                }

                return StatusCode((int)successCode, data);
            }
            catch (Exception ex)
            {
                //TODO: Implement Serilog
                //Logger.LogError(ex, $"An exception was thrown while executing the method {function.Method.Name}");

                return StatusCode(500, new Response(notifications: ex.FromHierarchy(e => e.InnerException)
                    .Select(e => e.Message)));
            }
        }


    }
}
