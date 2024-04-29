using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolAPI.Exceptions.Conflict;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Models.DTOs;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolAPI.Filters
{
    public class StudentFilterAttribute : ExceptionFilterAttribute
    {
        ErrorResponse errorResponse;
        Dictionary<string, List<string>> errors;

        public override void OnException(ExceptionContext context)
        {
            errorResponse = new ErrorResponse();
            errors = new Dictionary<string, List<string>>();
            if (context.Exception is NotFoundException)
            {
                NotFoundException notFound = (NotFoundException) context.Exception;

                errors.Add(notFound.Field, new List<string>() { notFound.Value });

                errorResponse.Message = notFound.Message;
                errorResponse.Errors = errors;

                context.Result = new NotFoundObjectResult(errorResponse);
            }
            else if (context.Exception is ConflictException)
            {
                ConflictException conflict = (ConflictException) context.Exception;

                errors.Add(conflict.Field, new List<string>() { conflict.Value });

                errorResponse.Message = conflict.Message;
                errorResponse.Errors = errors;

                context.Result = new ConflictObjectResult(errorResponse);
            }
        }
    }
}
