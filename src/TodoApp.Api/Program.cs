using Domain.Todos;
using Infra;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Usecase.Todos.Add;
using TodoApp.Api.Usecase.Todos.FindById;
using TodoApp.Api.Usecase.Todos.GetStatus;
using TodoApp.Api.Usecase.Todos.StartTodo;
using TodoApp.API.Apis;

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

builder.Services.AddScoped<IAddTodoUsecase, AddTodoUsecase>();
builder.Services.AddScoped<IFindByIdUsecase, FindByIdUsecase>();
builder.Services.AddScoped<IGetStatusUsecase, GetStatusUsecase>();
builder.Services.AddScoped<IStartTodoUsecase, StartTodoUsecase>();

builder.Services.AddScoped<ITodoReposity, TodoRepository>();


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

app.MapFallbackToFile("index.html");

app.Run();
