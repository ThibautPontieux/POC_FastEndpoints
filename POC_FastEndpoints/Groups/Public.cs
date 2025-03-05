using FastEndpoints;
using Microsoft.AspNetCore.Http;
using POC_FastEndpoints.Constants;
using POC_FastEndpoints.Routers;

namespace POC_FastEndpoints.Groups;

public class Public : SubGroup<BaseRouter>
{
    public Public()
    {
        Configure("public", ep =>
        {
            ep.Description(x => x.Produces(401).WithTags("public"));
            ep.AuthSchemes(FakeAuthConstant.PublicSchemeName);
        });
    }
}