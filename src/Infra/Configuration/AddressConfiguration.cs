using Domain.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Configuration;

internal static class AddressConfiguration
{
    public static void ConfigureAddress<TEntity>(this OwnedNavigationBuilder<TEntity, Address> addressBuilder)
        where TEntity : class
    {
        addressBuilder.Property(a => a.ZipCode).HasColumnName("zip_code");
    }
}
