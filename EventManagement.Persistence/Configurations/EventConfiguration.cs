using EventManagement.Domain.Entities.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Persistence.Configurations
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Navigation(e => e.AppliedUsers)
                .HasField(Event.AppliedUsersFieldName)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(255);

            builder.Property(e => e.Address)
                .HasColumnType("nvarchar")
                .HasMaxLength(255);

            builder.Property(e => e.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(4000);
        }
    }
}
