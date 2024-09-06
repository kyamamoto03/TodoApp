using Domain.UserModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user_info");
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.UserName).HasColumnName("user_name");
        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.IsStarted).HasColumnName("is_started");
        builder.Property(x => x.CreateDate).HasColumnName("create_date");
        builder.Property(x => x.UpdateDate).HasColumnName("update_date");
    }
}