namespace POC_FastEndpoints.Services;

public class HelloService : IHelloService
{
    public string SayHello(string? name, int age)
    {
        return $"Hello, {name}! You are {age} years old.";
    }
}