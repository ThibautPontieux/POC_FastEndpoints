using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using POC_FastEndpoints.Constants;

namespace POC_FastEndpoints.Handlers;

public class FakePublicAuthenticationSchemeHandler(
    IOptionsMonitor<FakeAuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<FakeAuthenticationSchemeOptions>(options, logger, encoder)
{
    private static readonly string[] Value = ["1", "2"];

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.Name, "FakeUser"),
            new Claim(ClaimTypes.Email, "JohnDoe@claim.com"),
            new Claim(FakeAuthConstant.SglkClaimClientIds, string.Join(',', Value)),
        ];
        // var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, FakeAuthConstant.PublicSchemeName));
        var principal = new ClaimsPrincipal(new ClaimsIdentity(Array.Empty<Claim>(), FakeAuthConstant.PublicSchemeName));
        AuthenticationTicket ticket = new(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}