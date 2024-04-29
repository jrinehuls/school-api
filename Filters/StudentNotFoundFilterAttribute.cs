using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolAPI.Exceptions.NotFound;
using System.Net;

namespace SchoolAPI.Filters
{
    public class StudentNotFoundFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is StudentNotFoundException)
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
            }
        }
    }
}
