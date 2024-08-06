namespace TodoApp.Client.PageModel;


public class HomePageModel() 
{
    private List<Todo.Domain.Todo> _todos = new();

    public List<Todo.Domain.Todo> Todos => _todos;

    public void CreateTodo()
    {
        Todo.Domain.Todo todo = Todo.Domain.Todo.CreateNew("TITLE", "DESCRIPTION", new DateTime(2024, 8, 1), new DateTime(2024, 8, 2));
        Todo.Domain.TodoItem todoItem = Todo.Domain.Todo.CreateNewTodoItem("ItemTitle", new DateTime(2024, 8, 1, 10, 0, 0), new DateTime(2024, 8, 2, 15, 0, 0));


        todo.AddTodoItem(todoItem);

        Todos.Add(todo);
    }

}
