var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

int requestCount = 0;
app.Use(async (context, next) =>
{
    requestCount++;
    await next(context);
    if (!context.Response.HasStarted)
    {
        await context.Response.WriteAsync($"The amount of processed requests is {requestCount}.\n");
    }
});

app.Use(async (context, next) =>
{
    if (context.Request.Query.ContainsKey("custom"))
    {
        await context.Response.WriteAsync("You’ve hit a custom middleware!");
        return; 
    }
    await next(context);
});

app.Use(async (context, next) =>
{
    Console.WriteLine($"{context.Request.Method} {context.Request.Path}");
    await next(context);
});

app.Use(async (context, next) =>
{
    const string validApiKey = "secret-key";
    if (!context.Request.Headers.TryGetValue("X-API-KEY", out var apiKey) || apiKey != validApiKey)
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Invalid or missing API key.");
        return;
    }
    await next(context);
});

app.MapGet("/", () => "Welcome to MiddlewareSandbox!");

app.Run();