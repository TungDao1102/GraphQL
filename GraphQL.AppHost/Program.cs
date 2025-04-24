var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.GraphQL_API>("graphql-api");

builder.Build().Run();
