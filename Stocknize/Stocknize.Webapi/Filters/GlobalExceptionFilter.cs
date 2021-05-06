using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Stocknize.Domain.Interfaces.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Stocknize.Webapi.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private const string DefaultErrorMessage = "Houve um erro inesperado!";
        private readonly IEnumerable<IExceptionHandler> exceptionHandlers;
        private readonly ILogger<ExceptionContext> logger;

        public GlobalExceptionFilter(IEnumerable<IExceptionHandler> exceptionHandlers,
            ILogger<ExceptionContext> logger)
        {
            this.exceptionHandlers = exceptionHandlers;
            this.logger = logger;
        }
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            logger.LogError(context.Exception.Message);
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
