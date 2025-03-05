using FastEndpoints;
using Microsoft.AspNetCore.Http;
using POC_FastEndpoints.Constants;
using POC_FastEndpoints.Routers;

namespace POC_FastEndpoints.Groups;

public class Private : SubGroup<BaseRouter>
{
    public Private()
    {
        Configure("private", ep =>
        {
            ep.Description(x => x.Produces(401).WithTags("private"));
            ep.AuthSchemes(FakeAuthConstant.PrivateSchemeName);
        });
    }
}