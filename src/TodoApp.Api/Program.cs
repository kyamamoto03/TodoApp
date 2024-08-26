using Domain.TodoModel;
using Domain.UserModel;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Apis;
using TodoApp.Api.Usecase;

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
    options.UseNpgsql("Server=localhost;Database=test;Port=51556;User Id=user;Password=pass");
});

#endregion

#region MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Programs).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Usecase).Assembly));

#endregion

builder.Services.AddScoped<TodoApp.Api.Usecase.TodoUsecase.Add.IAddTodoUsecase, TodoApp.Api.Usecase.TodoUsecase.Add.AddTodoUsecase>();
builder.Services.AddScoped<TodoApp.Api.Usecase.TodoUsecase.FindById.IFindByIdUsecase, TodoApp.Api.Usecase.TodoUsecase.FindById.FindByIdUsecase>();
builder.Services.AddScoped<TodoApp.Api.Usecase.TodoUsecase.GetStatus.IGetStatusUsecase, TodoApp.Api.Usecase.TodoUsecase.GetStatus.GetStatusUsecase>();
builder.Services.AddScoped<TodoApp.Api.Usecase.TodoUsecase.StartTodo.IStartTodoUsecase, TodoApp.Api.Usecase.TodoUsecase.StartTodo.StartTodoUsecase>();
builder.Services.AddScoped<TodoApp.Api.Usecase.UserUsecase.GetAll.IGetAllUsecase, TodoApp.Api.Usecase.UserUsecase.GetAll.GetAllUsecase>();
builder.Services.AddScoped<TodoApp.Api.Usecase.UserUsecase.StartTodo.IFirstTodoStartUsecase, TodoApp.Api.Usecase.UserUsecase.StartTodo.FirstTodoStartUsecase>();
builder.Services.AddScoped<TodoApp.Api.Usecase.UserUsecase.Add.IAddUsecase, TodoApp.Api.Usecase.UserUsecase.Add.AddUsecase>();

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