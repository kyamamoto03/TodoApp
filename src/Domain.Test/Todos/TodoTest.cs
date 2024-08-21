namespace Domain.TodoModel;

public class TodoTest
{
    [Fact]
    public void CreateNewしたTodoのTodoItemStatusが未開始になっているか()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId,"TodoTitle", "TodoDescription", startDate, endDate);

        var todoItemId = Guid.NewGuid().ToString();

        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        Assert.Equal(TodoItemStatus.未開始, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItemの開始日を設定したらTodoItemStatusが進行中になっているか()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);

        var todoItemId = Guid.NewGuid().ToString();

        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        todo.TodoItems.First().TaskStart(DateTime.Now);

        Assert.Equal(TodoItemStatus.進行中, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItemの終了日を設定したらTodoItemStatusが完了になっているか()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);

        var todoItemId = Guid.NewGuid().ToString();

        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);
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

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);

        var todoItemId = Guid.NewGuid().ToString();

        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        var todoItemId2 = Guid.NewGuid().ToString();

        TodoItem todoItem2 = Todo.CreateTodoItem(todoItemId2, "TodoItemTitle2", startDate, endDate);

        todo.AddTodoItem(todoItem2);

        todo.TodoItems.First().TaskStart(DateTime.Now);

        Assert.Equal(TodoItemStatus.進行中, todo.TodoItemStatus);
    }

    [Fact]
    public void TodoItemの終了日を設定したらTodoItemStatusが完了になっているか_TodoItem2個()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);

        var todoItemId = Guid.NewGuid().ToString();

        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);
        todo.AddTodoItem(todoItem);

        var todoItemId2 = Guid.NewGuid().ToString();

        TodoItem todoItem2 = Todo.CreateTodoItem(todoItemId2, "TodoItemTitle2", startDate, endDate);
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

        var todoItemId = Guid.NewGuid().ToString();
        TodoItem todoItem = Todo.CreateTodoItem(todoItemId,"TodoItemTitle", startDate, endDate);

        todoItem.SetAmount(100);

        Assert.Equal(100, todoItem.Amount);
    }
    [Fact]
    public void 金額設定_税プラス_テスト()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);
        var todoItemId = Guid.NewGuid().ToString();
        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);

        todoItem.SetAmount(100);

        Assert.Equal(110, todoItem.TaxIncludedAmount);
    }

    [Fact]
    public void 金額合計テスト_TodoItem10個()
    {
        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);
        for (int i = 0; i < 10; i++)
        {
            var todoItemId = Guid.NewGuid().ToString();
            TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);

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

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);
        for (int i = 0; i < 10; i++)
        {
            var todoItemId = Guid.NewGuid().ToString();
            TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);

            todoItem.SetAmount(100);
            todo.AddTodoItem(todoItem);
        }

        Assert.Equal(1100, todo.TotalTaxIncludedAmount);
    }

    [Fact]
    public void 開始テスト_進行中になること()
    {

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);
        var todoItemId = Guid.NewGuid().ToString();
        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);

        todo.AddTodoItem(todoItem);

        //未開始を確認
        Assert.Equal(TodoItemStatus.未開始, todo.TodoItemStatus);

        //開始する
        var StartDate = DateTime.Now;
        todo.StartTodoItem(todoItem.TodoItemId, StartDate);


        //開始になったことを確認する
        Assert.Equal(TodoItemStatus.進行中, todo.TodoItemStatus);
    }

    [Fact]
    public void 終了テスト_完了になること()
    {

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(1);

        var todoId = Guid.NewGuid().ToString();
        Todo todo = Todo.Create(todoId, "TodoTitle", "TodoDescription", startDate, endDate);
        var todoItemId = Guid.NewGuid().ToString();
        TodoItem todoItem = Todo.CreateTodoItem(todoItemId, "TodoItemTitle", startDate, endDate);

        todo.AddTodoItem(todoItem);

        //未開始を確認
        Assert.Equal(TodoItemStatus.未開始, todo.TodoItemStatus);

        //開始する
        var StartDate = DateTime.Now;
        todo.StartTodoItem(todoItem.TodoItemId, StartDate);

        //開始になったことを確認する
        Assert.Equal(TodoItemStatus.進行中, todo.TodoItemStatus);

        //終了する
        var EndDate = DateTime.Now;
        todo.EndTodoItem(todoItem.TodoItemId, EndDate);

        //開始になったことを確認する
        Assert.Equal(TodoItemStatus.完了, todo.TodoItemStatus);
    }

}