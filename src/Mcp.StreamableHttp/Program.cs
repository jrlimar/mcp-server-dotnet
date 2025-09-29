using Mcp.Core.Clients;
using Mcp.Core.Tools;
using ModelContextProtocol.Protocol;

var builder = WebApplication.CreateBuilder(args);

var serverInfo = new Implementation { Name = "DotnetMCPServer.StreamableHttp", Version = "1.0.0" };
builder.Services
    .AddMcpServer(mcp =>
    {
        mcp.ServerInfo = serverInfo;
    })
    .WithHttpTransport()
    .WithToolsFromAssembly(typeof(LivrariaTools).Assembly);

builder.Services.AddHttpClient<LivroApiClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7261/api/");
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapMcp();
app.Run();
