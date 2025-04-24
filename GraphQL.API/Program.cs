using GraphQL.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddGraphQLServer().AddQueryType<Query>();


var app = builder.Build();

app.MapDefaultEndpoints();
app.MapGraphQL();

app.Run();

public class Query
{
    public string SayHello(string name = "world") => $"Hello {name}";
}
