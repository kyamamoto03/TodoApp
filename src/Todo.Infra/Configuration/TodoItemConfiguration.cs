using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain;

namespace Todo.Infra.Configuration;

internal class TodoItemConfiguration : IEntityTypeConfiguration<Domain.TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(x => x.TodoItemId);
        builder.Property(x => x.TodoItemId).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.Title).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.ScheduleStartDate).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.ScheduleEndDate).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.TodoItemStatus).UsePropertyAccessMode(PropertyAccessMode.Field);

    }
}
