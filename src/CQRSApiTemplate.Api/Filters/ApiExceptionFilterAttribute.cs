using CQRSApiTemplate.Application.Common.Behaviour;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CQRSApiTemplate.Api.Filters;

public class ApiExceptionFilterAttribute: ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationException validationException)
        {
            context.Result = new ObjectResult(validationException.Result);
        }
      
        base.OnException(context);
    }
}
