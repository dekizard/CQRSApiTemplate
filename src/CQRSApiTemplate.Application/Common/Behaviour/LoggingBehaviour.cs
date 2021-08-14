using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using CQRSApiTemplate.Application.Interfaces;

namespace CQRSApiTemplate.Application.Common.Behaviour
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUser _currentUser;

        public LoggingBehaviour(ICurrentUser currentUser, ILogger logger)
        {
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUser.GetUserId();

            _logger.LogInformation("Request: {@requestName} {@userId} {@request}", requestName, userId, request);
            return Task.CompletedTask;
        }
    }
}
