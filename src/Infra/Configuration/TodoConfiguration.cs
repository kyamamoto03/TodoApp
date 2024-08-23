using Domain.TodoModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration;

internal class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("todo");
        builder.HasKey(x => x.TodoId);
        builder.Property(x => x.TodoId).HasColumnName("todo_id");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.Title).HasColumnName("title");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.ScheduleStartDate).HasColumnName("schedule_start_date");
        builder.Property(x => x.ScheduleEndDate).HasColumnName("schedule_end_date");
        builder.HasMany(x => x.TodoItems).WithOne().HasForeignKey("TodoId");

    }
}
