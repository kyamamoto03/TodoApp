using Domain.TodoModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration;

internal class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("todo_item");
        builder.HasKey(x => x.TodoItemId);
        builder.Property(x => x.TodoItemId).HasColumnName("todo_item_id");
        builder.Property(x => x.TodoId).HasColumnName("todo_id"); // Add this line
        builder.Property(x => x.Title).HasColumnName("title");
        builder.Property(x => x.ScheduleStartDate).HasColumnName("schedule_start_date");
        builder.Property(x => x.ScheduleEndDate).HasColumnName("schedule_end_date");
        builder.Property(x => x.StartDate).HasColumnName("start_date") ;
        builder.Property(x => x.EndDate).HasColumnName("end_date");
        builder.Property(x => x.Amount).HasColumnName("amount");
    }
}
