using System.Security.Claims;
using FastEndpoints;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using POC_FastEndpoints.Constants;

namespace POC_FastEndpoints.Processors;

public class ClientIdChecker : IGlobalPreProcessor
{
    public async Task PreProcessAsync(IPreProcessorContext context, CancellationToken ct)
    {
        // Retrieve client id from header
        StringValues clientId = context.HttpContext.Request.Headers["X-CLIENT-ID"];

        // Retrieve auth type and client id from user claims
        string? authType = context.HttpContext.User.Identity?.AuthenticationType;
        Claim? claim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == FakeAuthConstant.SglkClaimClientIds);
        
        // Do not check client id for public scheme anonymous or private scheme
        switch (authType)
        {
            case FakeAuthConstant.PublicSchemeName when claim == null:
            case FakeAuthConstant.PrivateSchemeName:
                return;
        }

        // Check if client id is present in header
        if (clientId.Count != 1)
        {
            context.ValidationFailures.Add(new ValidationFailure("ClientID", 
                "Unable to retrieve client id from header"));
            await context.HttpContext.Response.SendErrorsAsync(context.ValidationFailures, cancellation: ct);
            return;
        }
        
        // Check if client id is present in user claims
        if (claim == null)
        {
            context.ValidationFailures.Add(new ValidationFailure("ClientID", 
                "Unable to retrieve client id from user claims"));
            await context.HttpContext.Response.SendErrorsAsync(context.ValidationFailures, cancellation: ct);
            return;
        }
        
        if (!claim.Value.Split(',').Contains(clientId.ToString()))
        {
            context.ValidationFailures.Add(new ValidationFailure("ClientID", 
                "Client id not found in user claims"));
            await context.HttpContext.Response.SendErrorsAsync(context.ValidationFailures, cancellation: ct);
        }
    }
}