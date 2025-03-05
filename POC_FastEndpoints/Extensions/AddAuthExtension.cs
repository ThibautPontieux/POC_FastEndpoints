using Microsoft.Extensions.DependencyInjection;
using POC_FastEndpoints.Constants;
using POC_FastEndpoints.Handlers;

namespace POC_FastEndpoints.Extensions;

public static class AddAuthExtension
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication()
            .AddScheme<FakeAuthenticationSchemeOptions, FakePublicAuthenticationSchemeHandler>(
                FakeAuthConstant.PublicSchemeName, options => { })
            .AddScheme<FakeAuthenticationSchemeOptions, FakePrivateAuthenticationSchemeHandler>(
                FakeAuthConstant.PrivateSchemeName, options => { })
            .AddScheme<FakeAuthenticationSchemeOptions, FakeApiAuthenticationSchemeHandler>(
                FakeAuthConstant.ApiSchemeName, options => { });
        
        return services;
    }
}