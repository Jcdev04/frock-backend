using Frock_backend.shared.Domain.Repositories;
using Frock_backend.shared.Infrastructure.Persistences.EFC.Configuration;
using Frock_backend.shared.Infrastructure.Persistences.EFC.Repositories;


using Frock_backend.stops.Domain.Model.Aggregates.Geographic;
using Frock_backend.stops.Domain.Repositories.Geographic;

using Microsoft.EntityFrameworkCore;


namespace Frock_backend.stops.Infrastructure.Repositories.Geographic
{
    public class LocalityRepository(AppDbContext context) : BaseStringRepository<Locality>(context), ILocalityRepository
    {
        public async Task<IEnumerable<Locality>> FindByFkIdDistrictAsync(string fkIdDistrict)
        {
            return await Context.Set<Locality>()
                .Where(f => f.FkIdDistrict == fkIdDistrict)
                .ToListAsync();
        }
    }
}
