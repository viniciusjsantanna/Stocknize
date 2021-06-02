using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Stocknize.Crosscutting.Extensions
{
    public static class LoggedUserExtension
    {
        public static Guid GetLoggedUserId(this HttpContext httpContext)
        {
            return Guid.Parse(httpContext.User.Claims.FirstOrDefault(e => e.Type.Contains("nameidentifier")).Value);
        }

        public static Guid GetLoggedUserCompany(this HttpContext httpContext)
        {
            return Guid.Parse(httpContext.User.Claims.FirstOrDefault(e => e.Type.Contains("locality")).Value);
        }
    }
}
