using CQRSApiTemplate.Application.Common.ResultModel;
using CQRSApiTemplate.Resources;
using FluentValidation;

namespace CQRSApiTemplate.Application.Common.Behaviour;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<TRequest> _logger;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(t => t.ValidateAsync(context, cancellationToken)));

            var failures = validationResults.Where(t => t.Errors.Any()).SelectMany(t => t.Errors).ToList();
            if (failures.Count > 0)
            {
                var messages = new List<Message>();
                foreach (var error in failures)
                {
                    messages.Add(new Message()
                    {
                        Text = error.ErrorMessage,
                        Type = MessageType.Warning
                    });

                }

                _logger.LogWarning("Model is not valid: {@request} {@messages}", request, messages);

                throw new ValidationException(Result.CreateFailed(SharedMessages.vldNotValidModel, messages));
            }
        }

        return await next();
    }
}
