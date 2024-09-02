using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Apis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region db

builder.Services.AddDbContext<TodoDbContext>(options =>
{
    options.UseNpgsql("Server=localhost;Database=postgres;Port=51556;User Id=user;Password=pass");
});

#endregion db

#region MediatR

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Programs).Assembly));

#endregion MediatR

builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();
var todo = app.MapTodoApiV1();
var user = app.MapUserApiV1();

app.MapFallbackToFile("index.html");

app.MapDefaultEndpoints();

app.Run();

public class Programs()
{ }