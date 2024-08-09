namespace Domain.Todos;

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

    [Fact]
    public void 金額設定テスト()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);

        todoItem.SetAmount(100);

        Assert.Equal(100, todoItem.Amount);
    }
    [Fact]
    public void 金額設定_税プラス_テスト()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);

        todoItem.SetAmount(100);

        Assert.Equal(110, todoItem.TaxIncludedAmount);
    }

    [Fact]
    public void 金額合計テスト_TodoItem10個()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        for (int i = 0; i < 10; i++)
        {
            TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);

            todoItem.SetAmount(100);
            todo.AddTodoItem(todoItem);
        }

        Assert.Equal(1000, todo.TotalAmount);
    }

    [Fact]
    public void 金額合計テスト_税プラス_TodoItem10個()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        Todo todo = Todo.CreateNew("TodoTitle", "TodoDescription", startDate, endDate);
        for (int i = 0; i < 10; i++)
        {
            TodoItem todoItem = Todo.CreateNewTodoItem("TodoItemTitle", startDate, endDate);

            todoItem.SetAmount(100);
            todo.AddTodoItem(todoItem);
        }

        Assert.Equal(1100, todo.TotalTaxIncludedAmount);
    }
}