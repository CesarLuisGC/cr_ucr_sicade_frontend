public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly List<ApiKey> _keys;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _keys = configuration.GetSection("ApiKey").Get<List<ApiKey>>()!;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var paths = new List<string> { "/api/security" };

        if (!paths.Contains(context.Request.Path.Value!.ToLower()))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("API Key is missing");
            return;
        }

        var validKey = _keys.Any(key => key.value == extractedApiKey);

        if (!validKey)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Invalid API Key");
            return;
        }

        await _next(context);
    }
}
