using Domain.Todos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration;

internal class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(x => x.TodoItemId);
        builder.Property(x => x.TodoItemId).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.Title).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.ScheduleStartDate).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.ScheduleEndDate).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.StartDate).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.EndDate).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
