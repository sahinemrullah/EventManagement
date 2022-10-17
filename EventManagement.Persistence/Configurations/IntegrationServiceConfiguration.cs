using EventManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Persistence.Configurations
{
    internal class IntegrationServiceConfiguration : IEntityTypeConfiguration<IntegrationService>
    {
        public void Configure(EntityTypeBuilder<IntegrationService> builder)
        {
            builder.Property(x => x.WebDomain)
                .HasColumnType("nvarchar")
                .HasMaxLength(255);

            builder.Property(x => x.CompanyName)
                .HasColumnType("nvarchar")
                .HasMaxLength(255);

            builder.Property(x => x.Email)
                .HasColumnType("nvarchar")
                .HasMaxLength(320);

            builder.Property(x => x.PasswordHash)
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
        }
    }
}
