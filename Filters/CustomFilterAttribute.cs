using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Exceptions.Conflict;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Exceptions;
using SchoolAPI.Models.DTOs;

namespace SchoolAPI.Filters
{
    public abstract class CustomFilterAttribute : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                NotFoundException ex = (NotFoundException)context.Exception;
                ErrorResponse errorResponse = HandleApiExceptionException(ex);
                context.Result = new NotFoundObjectResult(errorResponse);
            }
            else if (context.Exception is ConflictException)
            {
                ConflictException ex = (ConflictException)context.Exception;
                ErrorResponse errorResponse = HandleApiExceptionException(ex);
                context.Result = new ConflictObjectResult(errorResponse);
            }
        }

        protected static ErrorResponse HandleApiExceptionException(ApiException exception)
        {
            ErrorResponse errorResponse = new ErrorResponse();
            Dictionary<string, List<string>> errors = new()
            {
                { exception.Field, new List<string>() { exception.Value } }
            };

            errorResponse.Message = exception.Message;
            errorResponse.Errors = errors;

            return errorResponse;
        }
    }
}
