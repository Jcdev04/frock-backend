using Frock_backend.shared.Infrastructure.Persistences.EFC.Configuration.Extensions;

//AGREGATES
using Frock_backend.stops.Domain.Model.Aggregates;
using Frock_backend.stops.Domain.Model.Aggregates.Geographic;

using Frock_backend.transport_Company.Domain.Model.Aggregates;

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

            //COMPANY
            builder.Entity<Company>().HasKey(f => f.Id);
            builder.Entity<Company>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Company>().Property(f => f.Name).IsRequired();
            builder.Entity<Company>().Property(f => f.LogoUrl).IsRequired();
            builder.Entity<Company>().Property(f => f.FkIdUser).IsRequired(); // Luego modificar para definirlo como foreign key

            //REGION
            builder.Entity<Region>().HasKey(f => f.Id);
            builder.Entity<Region>().Property(f => f.Id).IsRequired(); // no se pone value generated on add porque eso lo maneja el seeder, son valores estaticos
            builder.Entity<Region>().Property(f => f.Name).IsRequired();

            //PROVINCE
            builder.Entity<Province>().HasKey(f => f.Id);
            builder.Entity<Province>().Property(f => f.Id).IsRequired();
            builder.Entity<Province>().Property(f => f.Name).IsRequired();
            builder.Entity<Province>()
                .HasOne<Region>()
                .WithMany()
                .HasForeignKey(p => p.FkIdRegion)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            //DISTRICT
            builder.Entity<District>().HasKey(f => f.Id);
            builder.Entity<District>().Property(f => f.Id).IsRequired();
            builder.Entity<District>().Property(f => f.Name).IsRequired();
            builder.Entity<District>()
                .HasOne<Province>()
                .WithMany()
                .HasForeignKey(d => d.FkIdProvince)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            //LOCALITY
            builder.Entity<Locality>().HasKey(f => f.Id);
            builder.Entity<Locality>().Property(f => f.Id).IsRequired();
            builder.Entity<Locality>().Property(f => f.Name).IsRequired();
            builder.Entity<Locality>()
                .HasOne<District>()
                .WithMany()
                .HasForeignKey(l => l.FkIdDistrict)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            //STOP
            builder.Entity<Stop>().HasKey(f => f.Id);
            builder.Entity<Stop>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Stop>().Property(f => f.Name).IsRequired();
            builder.Entity<Stop>().Property(f => f.GoogleMapsUrl).IsRequired();
            builder.Entity<Stop>().Property(f => f.ImageUrl).IsRequired();
            builder.Entity<Stop>().Property(f => f.Phone).IsRequired();
            builder.Entity<Stop>().Property(f => f.Address).IsRequired();
            builder.Entity<Stop>().Property(f => f.Reference).IsRequired();

            builder.Entity<Stop>()
                .HasOne<Company>() // Un Stop tiene una Company
                .WithMany() // Una Company tiene muchos Stops
                .HasForeignKey(l => l.FkIdCompany)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Stop>()
                .HasOne<Locality>() // Un Stop tiene una Locality
                .WithMany() // Una Locality tiene muchos Stops
                .HasForeignKey(f => f.FkIdLocality)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.UseSnakeCaseNamingConvention();
        }
    }
}
