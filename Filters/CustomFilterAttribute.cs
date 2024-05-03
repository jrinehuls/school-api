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
            if (context.Exception is NotFoundException notFound)
            {
                ErrorResponse errorResponse = HandleApiException(notFound);
                ObjectResult result = new (errorResponse)
                {
                    StatusCode = notFound.StatusCode
                };
                context.Result = result;
            }
            else if (context.Exception is ConflictException conflict)
            {
                ErrorResponse errorResponse = HandleApiException(conflict);
                ObjectResult result = new(errorResponse)
                {
                    StatusCode = conflict.StatusCode
                };
                context.Result = result;
            }
        }

        protected static ErrorResponse HandleApiException(ApiException exception)
        {
            ErrorResponse errorResponse = new()
            {
                Message = exception.Message,
                Errors = exception.Errors
            };

            return errorResponse;
        }
    }
}
