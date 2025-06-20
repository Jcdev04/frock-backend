using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Frock_backend.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Frock_backend.Profiles.Domain.Model.Aggregates;
using Frock_backend.IAM.Domain.Model.Aggregates;

namespace Frock_backend.Shared.Infrastructure.Persistence.EFC.Configuration
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {

        /// <summary>
        ///     On configuring the database context
        /// </summary>
        /// <remarks>
        ///     This method is used to configure the database context.
        ///     It also adds the created and updated date interceptor to the database context.
        /// </remarks>
        /// <param name="builder">
        ///     The option builder for the database context
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(builder);
        }

        /// <summary>
        ///     On creating the database model
        /// </summary>
        /// <remarks>
        ///     This method is used to create the database model for the application.
        /// </remarks>
        /// <param name="builder">
        ///     The model builder for the database context
        /// </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Profiles Context

            builder.Entity<Profile>().HasKey(p => p.Id);
            builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Profile>().OwnsOne(p => p.Name,
                n =>
                {
                    n.WithOwner().HasForeignKey("Id");
                    n.Property(p => p.FirstName).HasColumnName("FirstName");
                    n.Property(p => p.LastName).HasColumnName("LastName");
                });

            builder.Entity<Profile>().OwnsOne(p => p.Email,
            e =>
            {
                    e.WithOwner().HasForeignKey("Id");
                    e.Property(a => a.Address).HasColumnName("EmailAddress");
                });

            builder.Entity<Profile>().OwnsOne(p => p.Address,
                a =>
                {
                    a.WithOwner().HasForeignKey("Id");
                    a.Property(s => s.Street).HasColumnName("AddressStreet");
                    a.Property(s => s.Number).HasColumnName("AddressNumber");
                    a.Property(s => s.City).HasColumnName("AddressCity");
                    a.Property(s => s.PostalCode).HasColumnName("AddressPostalCode");
                    a.Property(s => s.Country).HasColumnName("AddressCountry");
                });

            // IAM Context

            builder.Entity<User>().HasKey(u => u.Id);
            builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(u => u.Username).IsRequired();
            builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
            builder.UseSnakeCaseNamingConvention();
        }
    }
}
