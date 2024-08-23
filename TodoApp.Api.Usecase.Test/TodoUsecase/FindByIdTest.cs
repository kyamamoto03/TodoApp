using Domain.SeedOfWork;
using Domain.TodoModel;
using Moq;
using TodoApp.Api.Usecase.Todos.FindById;

namespace TodoApp.Api.Usecase.Test.TodoUsecase;

public class FindByIdTest
{
    public Mock<ITodoReposity> GetTodoReposity()
    {
        var todoRepository = new Mock<ITodoReposity>();
        var unitOfWork = new Mock<IUnitOfWork>();

        todoRepository.Setup(x => x.AddAsync(It.IsAny<Todo>()));
        todoRepository.Setup(x => x.UnitOfWork).Returns(unitOfWork.Object);

        return todoRepository;
    }

    [Fact]
    public async Task 検索_あり_Test()
    {
        // Arrange
        var todoRepository = GetTodoReposity();

        var userId = Guid.NewGuid().ToString();
        var todoId = Guid.NewGuid().ToString();
        var title = "Test";
        var description = "TestDescription";
        var scheduleStartDate = DateTime.Parse("2024/08/01");
        var scheduleEndDate = DateTime.Parse("2024/08/02");
        var todo = Todo.Create(userId,
            todoId,
            title,
            description,
            scheduleStartDate,
            scheduleEndDate);

        todoRepository.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(todo);

        // Act
        var usecase = new FindByIdUsecase(todoRepository.Object);

        // Assert
        var findTodo = await usecase.ExecuteAsync(todoId);
        Assert.NotNull(findTodo);
        Assert.Equal(todoId, findTodo.TodoId);
        Assert.Equal(title, findTodo.Title);
        Assert.Equal(description, findTodo.Description);
        Assert.Equal(scheduleStartDate, findTodo.ScheduleStartDate);
        Assert.Equal(scheduleEndDate, findTodo.ScheduleEndDate);

    }

    [Fact]
    public async Task 検索_なし_Test()
    {
        // Arrange
        var todoRepository = GetTodoReposity();

        Todo todo = null;

        todoRepository.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(todo);

        var usecase = new FindByIdUsecase(todoRepository.Object);
        // Act
        var findTodo = await usecase.ExecuteAsync(Guid.NewGuid().ToString());

        // Assert
        Assert.Null(findTodo);

    }

}
