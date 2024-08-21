using Domain.SeedOfWork;

namespace Domain.TodoModel;

public class TodoItemStatus : Enumeration
{
    public static TodoItemStatus 未開始 = new TodoItemStatus(1, nameof(未開始));
    public static TodoItemStatus 進行中 = new TodoItemStatus(2, nameof(進行中));
    public static TodoItemStatus 完了 = new TodoItemStatus(3, nameof(完了));

    public TodoItemStatus(int id, string name) : base(id, name)
    {
    }
}
