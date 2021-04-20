using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Stocknize.Domain.Interfaces.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Stocknize.Webapi.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private const string DefaultErrorMessage = "Houve um error inesperado!";
        private readonly IEnumerable<IExceptionHandler> exceptionHandlers;

        public GlobalExceptionFilter(IEnumerable<IExceptionHandler> exceptionHandlers)
        {
            this.exceptionHandlers = exceptionHandlers;
        }
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var exceptionType = exceptionHandlers.Where(e => e.GetType().Name.Contains(context.Exception.GetType().Name)).FirstOrDefault();
            if (exceptionType is not null)
            {
              context.Result = await exceptionType.Handle(context.Exception);
            }
            else
            {
                context.Result = GenerateDefaultErrorMessage(DefaultErrorMessage);
            }
        }

        private IActionResult GenerateDefaultErrorMessage(string message)
        {
            return new ObjectResult(message) { StatusCode = HttpStatusCode.InternalServerError.GetHashCode() };
        }
    }
}
