var builder = DistributedApplication.CreateBuilder(args);

var pgPassword = builder.AddParameter("postgres-password");

var db = builder.AddPostgres("postgres", null, pgPassword, 15442)
    .WithDataVolume("tododb-data")
    .WithInitBindMount("./data")
    .WithPgWeb()
    .AddDatabase("tododb");

builder.AddProject<Projects.TodoApp_Api>("api")
    .WithReference(db);

builder.Build().Run();