using EventManagement.Domain.Entities.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventManagement.Persistence.Configurations
{
    internal class TicketEventConfiguration : IEntityTypeConfiguration<TicketEvent>
    {
        public void Configure(EntityTypeBuilder<TicketEvent> builder)
        {
            builder.Property(e => e.Price).HasColumnType("decimal(9, 2)");
        }
    }
}
