namespace MemorySignal.Admin;

public static class HttpClientUtil
{
    public static HttpClient RemoveAuthHeaders(this HttpClient client)
    {
        client.DefaultRequestHeaders.Remove("Token");
        return client;
    }

    public static HttpClient AddAuthHeaders(this HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Add("Token", token);
        return client;
    }
}
