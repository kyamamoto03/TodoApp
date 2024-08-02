using Microsoft.EntityFrameworkCore;
using Todo.Domain;

namespace Todo.Infra.Repository;

public class TodoRepository(TodoMemDbContext todoMemDbContext) : ITodoReposity
{
    private readonly TodoMemDbContext _todoMemDbContext = todoMemDbContext;

    public async Task DeleteAsync(string todoId)
    {
        var targetTodo = await _todoMemDbContext.Todos.SingleOrDefaultAsync(x => x.TodoId == todoId);
        if (targetTodo == null)
        {
            throw new ArgumentException("指定されたTodoが存在しません");
        }
        _todoMemDbContext.Todos.Remove(targetTodo);
        await _todoMemDbContext.SaveChangesAsync();
    }

    public Task<Domain.Todo?> FindByIdAsync(string todoId)
    {
        return _todoMemDbContext.Todos
            .Include(x => x.TodoItems)
            .SingleOrDefaultAsync(x => x.TodoId == todoId);
    }

    public async Task<Domain.Todo> SaveAsync(Domain.Todo todo)
    {
        _todoMemDbContext.Todos.Add(todo);
        await _todoMemDbContext.SaveChangesAsync();

        return todo;
    }
}
