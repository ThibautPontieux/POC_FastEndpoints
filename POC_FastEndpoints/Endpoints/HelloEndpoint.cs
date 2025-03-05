using FastEndpoints;
using POC_FastEndpoints.Groups;
using POC_FastEndpoints.Services;

namespace POC_FastEndpoints.Endpoints;

public class HelloEndpoint(IHelloService helloService) : Endpoint<RequestDto, ResponseDto>
{
    public override void Configure()
    {
        Get("/{Name}");
        Group<Private>();
    }
    
    public override Task HandleAsync(RequestDto request, CancellationToken cancellationToken)
    {
        return SendAsync(new ResponseDto { Message = helloService.SayHello(request.Name, request.Age)}, cancellation: cancellationToken);
    }
    
}

public class RequestDto
{
    // [DontBind(Source.QueryParam)] // This will prevent the Name property from being bound to the query string
    public string Name { get; init; } = string.Empty;
    public int Age { get; init; }
}

public class ResponseDto
{
    public string? Message { get; init; }
}