using ResultModel;
using System;

namespace CQRSApiTemplate.Application.Common.Behaviour
{
    public class ValidationException: Exception
    {
        public Result Result { get; set; }
        
        public ValidationException() : base()
        {
        }

        public ValidationException(Result result) : this()
        {
            Result = result;
        }
    }
}
