namespace WebApplication1;

public class RequestService : IRequestService
{
    public async Task<string> GetJsonAsync(string url)
    {
        HttpClient client = new HttpClient();
        var resp = await client.GetAsync(url);
        return await resp.Content.ReadAsStringAsync();
    }

    public void PostJson(string url)
    {
        throw new NotImplementedException();
    }
}