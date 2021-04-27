using System;

namespace Stocknize.Domain.Models.Jwt
{
    public class JwtModel
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
