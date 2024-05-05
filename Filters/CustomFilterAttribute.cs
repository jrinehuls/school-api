using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Exceptions.Conflict;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Exceptions;
using SchoolAPI.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SchoolAPI.Filters
{
    public abstract class CustomFilterAttribute : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException apiException)
            {
                ErrorResponse errorResponse = new(apiException.Message);
                ObjectResult result = new (errorResponse)
                {
                    StatusCode = apiException.StatusCode
                };
                context.Result = result;
            }
        }

    }
}
