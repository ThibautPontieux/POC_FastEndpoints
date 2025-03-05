using FastEndpoints;

namespace POC_FastEndpoints.Routers;

public sealed class BaseRouter : Group
{
    private const string BaseUri = "/rest";
    
    public BaseRouter()
    {
        Configure($"{BaseUri}", ep => {});
    }
}