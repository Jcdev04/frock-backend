using Frock_backend.access_and_identity.Domain.ValueObjects;
using Frock_backend.access_and_identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Frock_backend.access_and_identity.Infrastructure.Persistence
{
    public class AccessIdentityDbContext : DbContext
    {
        public AccessIdentityDbContext(DbContextOptions<AccessIdentityDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                    .HasConversion(
                        userId => userId.Value,
                        value => new userID(value))
                    .HasMaxLength(7);

                entity.Property(u => u.Email)
                    .HasConversion(
                        email => email.Value,
                        value => new Email(value))
                    .HasMaxLength(255);

                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.FirstName).HasMaxLength(100);
                entity.Property(u => u.LastName).HasMaxLength(100);
                entity.Property(u => u.PasswordHash).HasMaxLength(255);
                entity.Property(u => u.Role).HasConversion<string>();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
