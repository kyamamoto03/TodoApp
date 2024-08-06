namespace Domain.Todos;

public interface ITodoReposity
{
    Task<Todo?> FindByIdAsync(string todoId);
    Task<Todo> SaveAsync(Todo todo);
    Task DeleteAsync(string todoId);
}
