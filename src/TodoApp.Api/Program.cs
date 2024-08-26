using Domain.TodoModel;
using Domain.UserModel;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Apis;
using TodoApp.Api.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region db
builder.Services.AddDbContext<TodoDbContext>(options =>
{
    options.UseNpgsql("Server=localhost;Database=postgres;Port=51556;User Id=user;Password=pass");
});

#endregion

#region MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Programs).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Service).Assembly));

#endregion

builder.Services.AddScoped<TodoApp.Api.Service.TodoService.Add.IAddTodoService, TodoApp.Api.Service.TodoService.Add.AddTodoService>();
builder.Services.AddScoped<TodoApp.Api.Service.TodoService.FindById.IFindByIdService, TodoApp.Api.Service.TodoService.FindById.FindByIdService>();
builder.Services.AddScoped<TodoApp.Api.Service.TodoService.GetStatus.IGetStatusService, TodoApp.Api.Service.TodoService.GetStatus.GetStatusService>();
builder.Services.AddScoped<TodoApp.Api.Service.TodoService.StartTodo.IStartTodoService, TodoApp.Api.Service.TodoService.StartTodo.StartTodoService>();
builder.Services.AddScoped<TodoApp.Api.Service.UserService.GetAll.IGetAllService, TodoApp.Api.Service.UserService.GetAll.GetAllService>();
builder.Services.AddScoped<TodoApp.Api.Service.UserService.StartTodo.IFirstTodoStartService, TodoApp.Api.Service.UserService.StartTodo.FirstTodoStartService>();
builder.Services.AddScoped<TodoApp.Api.Service.UserService.Add.IAddService, TodoApp.Api.Service.UserService.Add.AddService>();

builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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

app.Run();

public class Programs() { }