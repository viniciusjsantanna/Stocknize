using Microsoft.AspNetCore.Mvc;
using Stocknize.Domain.Interfaces.Filters;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Stocknize.Domain.Exceptions.NotFound
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {
        public Task<ObjectResult> Handle(Exception exception)
        {
            return Task.FromResult(new ObjectResult(exception.Message) { StatusCode = HttpStatusCode.InternalServerError.GetHashCode() });
        }
    }
}
