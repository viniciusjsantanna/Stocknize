using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Stocknize.Domain.Interfaces.Filters
{
    public interface IExceptionHandler
    {
        Task<ObjectResult> Handle(Exception exception);
    }
}
