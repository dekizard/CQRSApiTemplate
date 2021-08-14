using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CQRSApiTemplate.Application.Interfaces;

namespace CQRSApiTemplate.Application.Common.Behaviour
{
    public class PerformanceBehaviour<TRequest, TResonse> : IPipelineBehavior<TRequest, TResonse>
    {
        private readonly Stopwatch _timer;
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour(ICurrentUser currentUser, ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<TResonse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResonse> next)
        {
            _logger.LogInformation("Request: {@request}", request);

            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapseMiliseconds = _timer.ElapsedMilliseconds;

            if (elapseMiliseconds > 1000)
            {
                var requestName = typeof(TRequest).Name;
                var userId = _currentUser.GetUserId();

                _logger.LogWarning("Long Running Request: {@requestName} {@elapseMiliseconds} ms {@userId} {@request}", requestName, elapseMiliseconds, userId, request);
            }

            _logger.LogInformation("Response: {@response}", response);

            return response;
        }
    }
}
