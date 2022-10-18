using EventManagement.Domain.Common;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Entities.Events;
using EventManagement.Domain.Entities.Users;
using EventManagement.Persistence.DataSeeds;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EventManagement.Persistence
{
    public class EventManagementDbContext : DbContext, IUnitOfWork
    {
        public DbSet<IntegrationService> IntegrationServices { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<TicketEvent> TicketEvents { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<EventParticipant> EventParticipants { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public EventManagementDbContext(DbContextOptions<EventManagementDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.CreatedEvents)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            builder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRole>(ur => ur.HasOne(ur => ur.Role)
                                                       .WithMany()
                                                       .HasForeignKey(ur => ur.RoleId),
                                               ur => ur.HasOne(ur => ur.User)
                                                       .WithMany()
                                                       .HasForeignKey(ur => ur.UserId)
                                                       .OnDelete(DeleteBehavior.NoAction),
                                               ur => ur.HasKey(ur => new { ur.RoleId, ur.UserId }));

            builder.Entity<Event>()
                .HasMany(e => e.AppliedUsers)
                .WithMany(u => u.AppliedEvents)
                .UsingEntity<EventParticipant>(ep => ep.HasOne(ep => ep.User)
                                                       .WithMany()
                                                       .HasForeignKey(e => e.UserId),
                                               ep => ep.HasOne(e => e.Event)
                                                       .WithMany()
                                                       .HasForeignKey(e => e.EventId)
                                                       .OnDelete(DeleteBehavior.NoAction),
                                               ep => ep.HasKey(a => new { a.EventId, a.UserId }));

            builder.Entity<Category>().HasData(CategorySeed.Objects);
            builder.Entity<City>().HasData(CitySeed.Objects);
            builder.Entity<Role>().HasData(new { Id = 1, Name = "Admin" });
            builder.Entity<User>().HasData(UserSeed.Objects);
            builder.Entity<UserRole>().HasData(new { UserId = 1, RoleId = 1 });

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
