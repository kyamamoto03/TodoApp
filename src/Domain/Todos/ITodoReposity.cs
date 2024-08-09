namespace Domain.Todos;

public interface ITodoReposity
{
    /// <summary>
    /// TodoIdで検索
    /// </summary>
    /// <param name="todoId"></param>
    /// <returns></returns>
    Task<Todo?> FindByIdAsync(string todoId);

    /// <summary>
    /// 追加
    /// </summary>
    /// <param name="todo"></param>
    /// <returns></returns>
    Task<Todo> AddAsync(Todo todo);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="todo"></param>
    /// <returns></returns>
    Task UpdateAsync(Todo todo);

    /// <summary>
    /// 削除
    /// </summary>
    /// <param name="todoId"></param>
    /// <returns></returns>
    Task DeleteAsync(string todoId);
}
