using Domain.TodoModel;
using Moq;
using TodoApp.Api.Usecase.Todos.Add;


namespace TodoApp.Api.Usecase.Test.TodoUsecase;

public class AddTest
{
    [Fact]
    public async Task Add_Test()
    {
        // Arrange
        var todoRepository = new Mock<ITodoReposity>();
        var addTodoCommand = new AddTodoCommand()
        {
            TodoId = Guid.NewGuid().ToString(),
            Title = "Test",
            Description = "Test",
            ScheduleStartDate = DateTime.Now,
            ScheduleEndDate =  DateTime.Now,
            TodoItems = new AddTodoCommand.TodoItem[]
            {
                new AddTodoCommand.TodoItem
                {
                    TodoItemId = Guid.NewGuid().ToString(),
                    Title = "Test",
                    ScheduleStartDate = DateTime.Now,
                    ScheduleEndDate = DateTime.Now
                }
            }
        };

        todoRepository.Setup(x => x.AddAsync(It.IsAny<Todo>()));
        var usecase = new AddTodoUsecase(todoRepository.Object);

        // Act
        await usecase.ExecuteAsync(addTodoCommand);

        // Assert
        // AddAsyncが１回呼ばれていること
        todoRepository.Verify(x => x.AddAsync(It.IsAny<Todo>()), Times.Once);
    }
}
