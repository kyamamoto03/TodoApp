using TodoApp.Client.Dialog;

namespace TodoApp.Client.Test.PageModel
{
    public class CreateTodoDialogPageModelTest
    {
        [Fact]
        public void �S����NG_Test()
        {
            CreateTodoDialogPageModel createTodoDialogPageModel = new();

            CreateTodoDialogPageModelValidator createTodoDialogPageModelValidator = new();

            var result = createTodoDialogPageModelValidator.Validate(createTodoDialogPageModel);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void �S����OK_Test()
        {
            CreateTodoDialogPageModel createTodoDialogPageModel = new();

            createTodoDialogPageModel.Title = "�^�C�g��";
            createTodoDialogPageModel.ScheduleStartDate = DateTime.Now;
            createTodoDialogPageModel.ScheduleEndDate = DateTime.Now;

            CreateTodoDialogPageModelValidator createTodoDialogPageModelValidator = new();

            var result = createTodoDialogPageModelValidator.Validate(createTodoDialogPageModel);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void �^�C�g����_NG_Test()
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
        public void ScheduleStartDate��_NG_Test()
        {
            CreateTodoDialogPageModel createTodoDialogPageModel = new();

            createTodoDialogPageModel.Title = "�^�C�g��";
            createTodoDialogPageModel.ScheduleStartDate = null;
            createTodoDialogPageModel.ScheduleEndDate = DateTime.Now;

            CreateTodoDialogPageModelValidator createTodoDialogPageModelValidator = new();

            var result = createTodoDialogPageModelValidator.Validate(createTodoDialogPageModel);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ScheduleEndDate��_NG_Test()
        {
            CreateTodoDialogPageModel createTodoDialogPageModel = new();

            createTodoDialogPageModel.Title = "�^�C�g��";
            createTodoDialogPageModel.ScheduleStartDate = DateTime.Now;
            createTodoDialogPageModel.ScheduleEndDate = null;

            CreateTodoDialogPageModelValidator createTodoDialogPageModelValidator = new();

            var result = createTodoDialogPageModelValidator.Validate(createTodoDialogPageModel);

            Assert.False(result.IsValid);
        }
    }
}