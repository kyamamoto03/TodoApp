namespace TodoApp.Client.PageModel;

public interface IHomePageModel
{
    void CreateTodo();
}

public class HomePageModel() : IHomePageModel
{

    public void CreateTodo()
    {
        Todo.Domain.Todo todo = Todo.Domain.Todo.CreateNew("TITLE", "DESCRIPTION", new DateTime(2024, 8, 1), new DateTime(2024, 8, 2));
        Todo.Domain.TodoItem todoItem = Todo.Domain.Todo.CreateNewTodoItem("ItemTitle", new DateTime(2024, 8, 1, 10, 0, 0), new DateTime(2024, 8, 2, 15, 0, 0));


        todo.AddTodoItem(todoItem);
    }

}
