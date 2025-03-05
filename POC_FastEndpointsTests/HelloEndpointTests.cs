using FakeItEasy;
using FastEndpoints;
using POC_FastEndpoints.Endpoints;
using POC_FastEndpoints.Services;

namespace POC_FastEndpointsTests;

public class HelloEndpointTests
{
    [Fact]
    public async Task HandleAsync_Test_Success()
    {
        // Arrange
        var fakeHelloService = A.Fake<IHelloService>();
        A.CallTo(() => fakeHelloService.SayHello("John", 30))
            .Returns("Hello, John! You are 30 years old.");

        var endpoint = Factory.Create<HelloEndpoint>(fakeHelloService);
        
        var request = new RequestDto
        {
            Name = "John",
            Age = 30
        };
        
        // Act
        await endpoint.HandleAsync(request, CancellationToken.None);
        ResponseDto response = endpoint.Response;

        // Assert
        Assert.NotNull(response);
        Assert.False(endpoint.ValidationFailed);
        Assert.Equal("Hello, John! You are 30 years old.", response.Message);
    }
}