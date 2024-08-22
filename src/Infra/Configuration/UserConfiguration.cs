using Domain.UserModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.UserId).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.UserName).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Property(x => x.Email).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
