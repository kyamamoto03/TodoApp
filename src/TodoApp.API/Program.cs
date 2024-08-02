using Microsoft.EntityFrameworkCore;
using Todo.Domain;
using Todo.Infra;
using Todo.Infra.Repository;
using Todo.Usecase.Todos;
using TodoApp.API.APIs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region db
builder.Services.AddDbContext<TodoMemDbContext>(options =>
{
    options.UseInMemoryDatabase("TodoMemDb");
});

#endregion

builder.Services.AddScoped<IAddTodoUsecase, AddTodoUsecase>();
builder.Services.AddScoped<IFindByIdUsecase, FindByIdUsecase>();

builder.Services.AddScoped<ITodoReposity, TodoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var todo = app.MapTodoApiV1();

app.Run();
