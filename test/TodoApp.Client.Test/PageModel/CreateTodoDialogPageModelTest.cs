using TodoApp.Client.Dialog;

namespace TodoApp.Client.Test.PageModel
{
    public class CreateTodoDialogPageModelTest
    {
        [Fact]
        public void 全項目NG_Test()
        {
            CreateTodoDialogPageModel createTodoDialogPageModel = new();

            CreateTodoDialogPageModelValidator createTodoDialogPageModelValidator = new();

            var result = createTodoDialogPageModelValidator.Validate(createTodoDialogPageModel);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void 全項目OK_Test()
        {
            CreateTodoDialogPageModel createTodoDialogPageModel = new();

            createTodoDialogPageModel.Title = "タイトル";
            createTodoDialogPageModel.ScheduleStartDate = DateTime.Now;
            createTodoDialogPageModel.ScheduleEndDate = DateTime.Now;

            CreateTodoDialogPageModelValidator createTodoDialogPageModelValidator = new();

            var result = createTodoDialogPageModelValidator.Validate(createTodoDialogPageModel);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void タイトル空_NG_Test()
        {
            CreateTodoDialogPageModel createTodoDialogPageModel = new();

            createTodoDialogPageModel.Title = string.Empty;
            createTodoDialogPageModel.ScheduleStartDate = DateTime.Now;
            createTodoDialogPageModel.ScheduleEndDate = DateTime.Now;

            CreateTodoDialogPageModelValidator createTodoDialogPageModelValidator = new();

            var result = createTodoDialogPageModelValidator.Validate(createTodoDialogPageModel);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ScheduleStartDate空_NG_Test()
        {
            CreateTodoDialogPageModel createTodoDialogPageModel = new();

            createTodoDialogPageModel.Title = "タイトル";
            createTodoDialogPageModel.ScheduleStartDate = null;
            createTodoDialogPageModel.ScheduleEndDate = DateTime.Now;

            CreateTodoDialogPageModelValidator createTodoDialogPageModelValidator = new();

            var result = createTodoDialogPageModelValidator.Validate(createTodoDialogPageModel);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ScheduleEndDate空_NG_Test()
        {
            CreateTodoDialogPageModel createTodoDialogPageModel = new();

            createTodoDialogPageModel.Title = "タイトル";
            createTodoDialogPageModel.ScheduleStartDate = DateTime.Now;
            createTodoDialogPageModel.ScheduleEndDate = null;

            CreateTodoDialogPageModelValidator createTodoDialogPageModelValidator = new();

            var result = createTodoDialogPageModelValidator.Validate(createTodoDialogPageModel);

            Assert.False(result.IsValid);
        }
    }
}