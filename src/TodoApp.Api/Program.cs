using Domain.TodoModel;
using Domain.UserModel;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Apis;

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
    options.UseInMemoryDatabase("TodoMemDb");
});

#endregion

builder.Services.AddScoped<TodoApp.Api.Usecase.Todos.Add.IAddTodoUsecase, TodoApp.Api.Usecase.Todos.Add.AddTodoUsecase>();
builder.Services.AddScoped<TodoApp.Api.Usecase.Todos.FindById.IFindByIdUsecase, TodoApp.Api.Usecase.Todos.FindById.FindByIdUsecase>();
builder.Services.AddScoped<TodoApp.Api.Usecase.Todos.GetStatus.IGetStatusUsecase, TodoApp.Api.Usecase.Todos.GetStatus.GetStatusUsecase>();
builder.Services.AddScoped<TodoApp.Api.Usecase.Todos.StartTodo.IStartTodoUsecase, TodoApp.Api.Usecase.Todos.StartTodo.StartTodoUsecase>();
builder.Services.AddScoped<TodoApp.Api.Usecase.User.GetAll.IGetAllUsecase, TodoApp.Api.Usecase.User.GetAll.GetAllUsecase>();

builder.Services.AddScoped<ITodoReposity, TodoRepository>();
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
