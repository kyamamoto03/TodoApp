namespace Todo.Domain.Test.Domain;

public class TodoTest
{
    [Fact]
    public void CreateNewしたTodoのTodoItemStatusが未開始になっているか()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        todo.AddTodoItem(todoItem);

        Assert.Equal(TodoItemStatus.未開始, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItemの開始日を設定したらTodoItemStatusが進行中になっているか()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        todo.TodoItems.First().TaskStart(DateTime.Now);

        Assert.Equal(TodoItemStatus.進行中, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItemの終了日を設定したらTodoItemStatusが完了になっているか()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);

        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        todo.TodoItems.First().TaskStart(DateTime.Now);
        todo.TodoItems.First().TaskEnd(DateTime.Now);

        Assert.Equal(TodoItemStatus.完了, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItemの開始日を設定したらTodoItemStatusが進行中になっているか_TodoItem2個()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        TodoItem todoItem2 = Todo.CreateNewTodoItem("TodoItemTitle2", startDate, endDate);

        todo.AddTodoItem(todoItem2);

        todo.TodoItems.First().TaskStart(DateTime.Now);

        Assert.Equal(TodoItemStatus.進行中, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItemの終了日を設定したらTodoItemStatusが完了になっているか_TodoItem2個()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        TodoItem todoItem2 = Todo.CreateNewTodoItem("TodoItemTitle2", startDate, endDate);
        todo.AddTodoItem(todoItem2);

        // すべてのTodoItemを開始し終了する
        foreach (var item in todo.TodoItems)
        {
            item.TaskStart(DateTime.Now);
            item.TaskEnd(DateTime.Now);
        }

        Assert.Equal(TodoItemStatus.完了, todo.TodoItemStatus);
    }

}