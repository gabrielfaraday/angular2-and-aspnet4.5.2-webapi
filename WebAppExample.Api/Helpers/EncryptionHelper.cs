using JWT;
using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using WebAppExample.Lib.Data.Models;

namespace WebAppExample.Api.Helpers
{
    public static class EncryptionHelper
    {
        public static string EncryptPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var passwordAsBytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(sha256.ComputeHash(passwordAsBytes));
            }
        }

        public static string CreateToken(User user)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var expiry = Math.Round((DateTime.UtcNow.AddHours(2) - unixEpoch).TotalSeconds);
            var issuedAt = Math.Round((DateTime.UtcNow - unixEpoch).TotalSeconds);
            var notBefore = Math.Round((DateTime.UtcNow.AddMonths(6) - unixEpoch).TotalSeconds);

            var payload = new Dictionary<string, object>
            {
                {"userName", user.UserName},
                {"role", "Admin"  },
                {"sub", user.UserName},
                {"nbf", notBefore},
                {"iat", issuedAt},
                {"exp", expiry}
            };

            const string apikey = "secretKey";

            return JsonWebToken.Encode(payload, apikey, JwtHashAlgorithm.HS256);
        }
    }
}