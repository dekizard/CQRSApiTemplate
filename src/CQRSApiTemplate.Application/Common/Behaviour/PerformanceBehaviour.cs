using CQRSApiTemplate.Application.Interfaces;
using System.Diagnostics;

namespace CQRSApiTemplate.Application.Common.Behaviour;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
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

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation("Request: {@request}", request);

        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapseMilliseconds = _timer.ElapsedMilliseconds;

        if (elapseMilliseconds > 1000)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUser.GetUserId();

            _logger.LogWarning("Long Running Request: {@requestName} {@elapseMilliseconds} ms {@userId} {@request}", requestName, elapseMilliseconds, userId, request);
        }

        return response;
    }
}
