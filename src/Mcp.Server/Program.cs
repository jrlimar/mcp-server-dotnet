using Mcp.Core.Clients;
using Mcp.Core.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Debug;
});

var serverInfo = new Implementation { Name = "DotnetMCPServer.Stdio", Version = "1.0.0" };
builder.Services
    .AddMcpServer(mcp =>
    {
        mcp.ServerInfo = serverInfo;
    })
    .WithStdioServerTransport()
    .WithToolsFromAssembly(typeof(LivrariaTools).Assembly);

builder.Services.AddHttpClient<LivroApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7261/api/");
});

var app = builder.Build();
await app.RunAsync();
