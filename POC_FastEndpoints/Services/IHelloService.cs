namespace POC_FastEndpoints.Services;

public interface IHelloService
{
    string SayHello(string? name, int age);
}