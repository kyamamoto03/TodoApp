using Domain.SeedOfWork;
using Domain.UserModel;
using Moq;

namespace TodoApp.Api.Usecase.Test.UserUsecas;

public class FirstTodoStartUsecaseTest
{
    public Mock<IUserRepository> GetUserReposity()
    {
        var todoRepository = new Mock<IUserRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        todoRepository.Setup(x => x.UpdateAsync(It.IsAny<User>()));
        todoRepository.Setup(x => x.UnitOfWork).Returns(unitOfWork.Object);

        return todoRepository;
    }

    [Fact]
    public async Task FirstTodoStart_Test()
    {
        // Arrange
        var userRepository = GetUserReposity();


        var todoId = Guid.NewGuid().ToString();
        //var user = User.Create(todoId,
    }
}
