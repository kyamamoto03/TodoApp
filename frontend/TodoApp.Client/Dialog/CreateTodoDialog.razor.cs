using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace TodoApp.Client.Dialog;

public partial class CreateTodoDialog
{
    [CascadingParameter]
    IMudDialogInstance MudDialog { get; set; } = default!;

    private CreateTodoDialogPageModel createTodoDialogPageModel = new();
    private bool _isProcessing { get; set; } = false;

    private FluentValidationValidator? _fluentValidationValidator;

    private async Task CreateTodo()
    {
        try
        {
            _isProcessing = true;

            if (_fluentValidationValidator != null && await _fluentValidationValidator.ValidateAsync())
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
        finally
        {
            _isProcessing = false;
        }
    }
}