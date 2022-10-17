using EventManagement.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Navigation(u => u.AppliedEvents)
                .HasField(User.Mapping.AppliedEventsFieldName)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(u => u.CreatedEvents)
                .HasField(User.Mapping.CreatedEventsFieldName)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(u => u.Roles)
                .HasField(User.Mapping.RoleFieldName)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(u => u.FirstName)
                .HasColumnType("nvarchar")
                .HasMaxLength(30);

            builder.Property(u => u.LastName)
                .HasColumnType("nvarchar")
                .HasMaxLength(30);

            builder.Property(u => u.Email)
                .HasColumnType("nvarchar")
                .HasMaxLength(320);

            builder.Property(u => u.PasswordHash)
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
        }
    }
}
