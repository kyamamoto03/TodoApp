namespace TodoApp.Client.Domain.TodoModel;

public class TodoItem
{
    public string TodoItemId { get; internal set; } = default!;
    public string TodoId { get; internal set; } = default!;
    public string Title { get; internal set; } = default!;
    public DateTime ScheduleStartDate { get; internal set; } = default!;
    public DateTime ScheduleEndDate { get; internal set; } = default!;
    public DateTime? StartDate { get; internal set; } = default!;
    public DateTime? EndDate { get; internal set; } = default!;

    public TodoItemStatus TodoItemStatus
    {
        get
        {
            if (StartDate is null && EndDate is null)
            {
                return TodoItemStatus.未開始;
            }
            else if (StartDate is not null && EndDate is null)
            {
                return TodoItemStatus.進行中;
            }
            else
            {
                return TodoItemStatus.完了;
            }
        }
    }

    /// <summary>
    /// 税抜き金額
    /// </summary>
    public decimal Amount { get; internal set; } = 0;

    public decimal TaxIncludedAmount
    {
        get
        {
            return AddTax(Amount);
        }
    }

    internal TodoItem()
    {
    }

    public void TaskStart(DateTime startDate)
    {
        StartDate = startDate;
    }

    public void TaskEnd(DateTime endDate)
    {
        if (StartDate == null)
        {
            throw new ArgumentException("開始日が設定されていません");
        }
        if (StartDate > endDate)
        {
            throw new ArgumentException("開始日よりも前の日付は設定できません");
        }
        EndDate = endDate;
    }

    public void SetAmount(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("金額は0以上を設定してください");
        }
        Amount = amount;
    }

    public readonly decimal TAX = 0.1m;

    //消費税TAXを加算するメソッド
    public decimal AddTax(decimal amount)
    {
        return amount * (1 + TAX);
    }
}