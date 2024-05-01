namespace WebApplication1;

public interface IRequestService
{
    Task<string> GetJsonAsync(string url);
    void PostJson(string url);
}