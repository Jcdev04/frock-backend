using Frock_backend.shared.Domain.Repositories;
using Frock_backend.stops.Domain.Model.Aggregates.Geographic;

namespace Frock_backend.stops.Domain.Repositories.Geographic
{
    public interface ILocalityRepository : IBaseStringRepository<Locality>
    {
        Task<IEnumerable<Locality>> FindByFkIdDistrictAsync(string fkIdDistrict);
    }
}
