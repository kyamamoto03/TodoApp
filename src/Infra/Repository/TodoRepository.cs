using Domain.Todos;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository;

public class TodoRepository(TodoDbContext todoMemDbContext) : ITodoReposity
{
    private readonly TodoDbContext _todoDbContext = todoMemDbContext;

    public async Task DeleteAsync(string todoId)
    {
        var targetTodo = await _todoDbContext.Todos.SingleOrDefaultAsync(x => x.TodoId == todoId);
        if (targetTodo == null)
        {
            throw new ArgumentException("指定されたTodoが存在しません");
        }
        _todoDbContext.Todos.Remove(targetTodo);
        await _todoDbContext.SaveChangesAsync();
    }

    public Task<Todo?> FindByIdAsync(string todoId)
    {
        return _todoDbContext.Todos
            .Include(x => x.TodoItems)
            .FirstOrDefaultAsync(x => x.TodoId == todoId);
    }

    public async Task<Todo> AddAsync(Todo todo)
    {
        _todoDbContext.Todos.Add(todo);
        await _todoDbContext.SaveChangesAsync();

        return todo;
    }

    public async Task UpdateAsync(Todo todo)
    {
        _todoDbContext.Entry(todo).State = EntityState.Modified;
        await _todoDbContext.SaveChangesAsync();
    }
}
