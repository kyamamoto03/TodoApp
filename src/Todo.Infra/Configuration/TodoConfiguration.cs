using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Todo.Infra.Configuration;

internal class TodoConfiguration : IEntityTypeConfiguration<Domain.Todo>
{
    public void Configure(EntityTypeBuilder<Domain.Todo> builder)
    {
        builder.HasKey(x => x.TodoId);
        builder.Property(x => x.TodoId).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.Title).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.Description).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.ScheduleStartDate).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.ScheduleEndDate).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.HasMany(x => x.TodoItems).WithOne().HasForeignKey("TodoId");

    }
}
