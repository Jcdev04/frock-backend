using Frock_backend.stops.Domain.Model.Aggregates.Geographic;
using Frock_backend.stops.Domain.Model.Commands.Geographic;
using Frock_backend.stops.Domain.Model.Queries.Geographic;
using Frock_backend.stops.Domain.Services.Geographic;
using Microsoft.Extensions.Logging;

namespace Frock_backend.stops.Infrastructure.Seeding
{
    public class GeographicDataSeeder
    {
        private readonly IRegionCommandService _regionCommandService;
        private readonly IRegionQueryService _regionQueryService;
        private readonly IProvinceCommandService _provinceCommandService;
        private readonly IDistrictCommandService _districtCommandService;
        private readonly ILocalityCommandService _localityCommandService;
        private readonly ILogger<GeographicDataSeeder> _logger;

        public GeographicDataSeeder(
            IRegionCommandService regionCommandService,
            IRegionQueryService regionQueryService,
            IProvinceCommandService provinceCommandService,
            IDistrictCommandService districtCommandService,
            ILocalityCommandService localityCommandService,
            ILogger<GeographicDataSeeder> logger)
        {
            _regionCommandService = regionCommandService;
            _regionQueryService = regionQueryService;
            _provinceCommandService = provinceCommandService;
            _districtCommandService = districtCommandService;
            _localityCommandService = localityCommandService;
            _logger = logger;
        }

        public async Task SeedDataAsync()
        {
            _logger.LogInformation("Iniciando la carga de datos geográficos...");

            // Verificar si ya existen datos
            var existingRegions = await _regionQueryService.Handle(new GetAllRegionsQuery());
            if (existingRegions.Any())
            {
                _logger.LogInformation("Los datos geográficos ya están cargados.");
                return;
            }

            // Cargar regiones
            await SeedRegionsAsync();
            
            // Cargar provincias
            await SeedProvincesAsync();
            
            // Cargar distritos
            await SeedDistrictsAsync();
            
            // Cargar localidades
            await SeedLocalitiesAsync();
            
            _logger.LogInformation("Carga de datos geográficos completada con éxito.");
        }

        private async Task SeedRegionsAsync()
        {
            _logger.LogInformation("Cargando regiones...");
            
            var regions = new List<CreateRegionCommand>
            {
                new("reg-1", "Madre De Dios"),
                new("reg-2", "Lima"),
                new("reg-3", "La Libertad"),
                new("reg-4", "Ancash"),
                new("reg-5", "Ica"),
                new("reg-6", "Puno"),
                new("reg-7", "Arequipa"),
                new("reg-8", "Cusco"),
                new("reg-9", "Huánuco"),
                new("reg-10", "Ayacucho"),
                new("reg-11", "Amazonas"),
                new("reg-12", "Lambayeque"),
                new("reg-13", "Tacna"),
                new("reg-14", "Junín")
                // Agrega más regiones según necesites
            };

            foreach (var region in regions)
            {
                await _regionCommandService.Handle(region);
            }
        }

        private async Task SeedProvincesAsync()
        {
            _logger.LogInformation("Cargando provincias...");
            
            var provinces = new List<CreateProvinceCommand>
            {
                new("prov-1", "Santiago", "reg-1"),
                new("prov-2", "Valparaíso", "reg-2"),
                new("prov-3", "Talca", "reg-3")
                // Agrega más provincias según necesites
            };

            foreach (var province in provinces)
            {
                await _provinceCommandService.Handle(province);
            }
        }

        private async Task SeedDistrictsAsync()
        {
            _logger.LogInformation("Cargando distritos...");
            
            var districts = new List<CreateDistrictCommand>
            {
                new("dist-1", "Santiago Centro", "prov-1"),
                new("dist-2", "Providencia", "prov-2"),
                new("dist-3", "Valparaíso Centro", "prov-3"),
                // Agrega más distritos según necesites
            };

            foreach (var district in districts)
            {
                await _districtCommandService.Handle(district);
            }
        }

        private async Task SeedLocalitiesAsync()
        {
            _logger.LogInformation("Cargando localidades...");
            
            var localities = new List<CreateLocalityCommand>
            {
                new("loc-1", "Plaza de Armas", "dist-1"),
                new("loc-2", "Barrio Bellavista", "dist-2"),
                new("loc-3", "Cerro Alegre", "dist-3"),
                // Agrega más localidades según necesites
            };

            foreach (var locality in localities)
            {
                await _localityCommandService.Handle(locality);
            }
        }
    }
}
