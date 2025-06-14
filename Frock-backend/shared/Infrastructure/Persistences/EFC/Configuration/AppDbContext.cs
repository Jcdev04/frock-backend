using Frock_backend.shared.Infrastructure.Persistences.EFC.Configuration.Extensions;
using Frock_backend.stops.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;


namespace Frock_backend.shared.Infrastructure.Persistences.EFC.Configuration
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.AddCreatedUpdatedInterceptor();
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Stop>().HasKey(f => f.Id);
            builder.Entity<Stop>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Stop>().Property(f => f.Name).IsRequired();
            builder.Entity<Stop>().Property(f => f.GoogleMapsUrl).IsRequired();
            builder.Entity<Stop>().Property(f => f.ImageUrl).IsRequired();
            builder.Entity<Stop>().Property(f => f.Phone).IsRequired();
            builder.Entity<Stop>().Property(f => f.FkIdCompany).IsRequired();
            builder.Entity<Stop>().Property(f => f.Address).IsRequired();
            builder.Entity<Stop>().Property(f => f.Reference).IsRequired();
            builder.Entity<Stop>().Property(f => f.FkIdLocality).IsRequired();

            builder.UseSnakeCaseNamingConvention();
        }
    }
}
