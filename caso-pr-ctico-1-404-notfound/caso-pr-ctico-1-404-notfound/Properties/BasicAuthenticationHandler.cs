using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API.Properties
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("Authorization", out var header) ||
                !AuthenticationHeaderValue.TryParse(header, out var headerValue) ||
                !string.Equals(headerValue.Scheme, "Basic", StringComparison.OrdinalIgnoreCase) ||
                string.IsNullOrEmpty(headerValue.Parameter))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            string decoded;
            try
            {
                decoded = Encoding.UTF8.GetString(Convert.FromBase64String(headerValue.Parameter));
            }
            catch
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalido"));
            }

            var parts = decoded.Split(':', 2);
            if (parts.Length != 2)
                return Task.FromResult(AuthenticateResult.Fail("Credenciales invalidas"));

            var username = parts[0];
            var password = parts[1];

            if (!(username == "admin" && password == "1234") &&
                !(username == "user" && password == "abcd"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Username o Contrasena invalidos"));
            }

            var role = username == "admin" ? "admin" : "user";
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,  username),
                new Claim(ClaimTypes.Role,  role),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
