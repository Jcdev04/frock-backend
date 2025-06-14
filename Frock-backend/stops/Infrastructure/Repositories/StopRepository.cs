using Frock_backend.shared.Domain.Repositories;
using Frock_backend.shared.Infrastructure.Persistences.EFC.Configuration;
using Frock_backend.shared.Infrastructure.Persistences.EFC.Repositories;


using Frock_backend.stops.Domain.Model.Aggregates;
using Frock_backend.stops.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Frock_backend.stops.Infrastructure.Repositories
{
    public class StopRepository(AppDbContext context) : BaseRepository<Stop>(context), IStopRepository
    {
        public async Task<IEnumerable<Stop>> FindByFkIdCompanyAsync(string fkIdCompany)
        {
            return await Context.Set<Stop>()
                .Where(f => f.FkIdCompany == fkIdCompany)
                .ToListAsync();
        }
        public async Task<IEnumerable<Stop>> FindByFkIdLocalityAsync(string fkIdLocality)
        {
            return await Context.Set<Stop>()
                .Where(f => f.FkIdLocality == fkIdLocality)
                .ToListAsync();
        }
        public async Task<Stop?> FindByNameAndFkIdLocalityAsync(string name, string fkIdLocality)
        {
            return await context.Set<Stop>()
                .FirstOrDefaultAsync(f => f.Name == name && f.FkIdLocality == fkIdLocality);
        }
    }
}
