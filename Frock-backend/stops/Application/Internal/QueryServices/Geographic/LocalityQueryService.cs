using Frock_backend.stops.Domain.Model.Aggregates.Geographic;
using Frock_backend.stops.Domain.Model.Queries.Geographic;
using Frock_backend.stops.Domain.Repositories.Geographic;
using Frock_backend.stops.Domain.Services.Geographic;

namespace Frock_backend.stops.Application.Internal.QueryServices.Geographic
{
    public class LocalityQueryService(ILocalityRepository localityRepository) : ILocalityQueryService
    {
        public async Task<IEnumerable<Locality>> Handle(GetAllLocalitiesQuery query)
        {
            return await localityRepository.ListAsync();
        }
        public async Task<Locality?> Handle(GetLocalityByIdQuery query)
        {
            return await localityRepository.FindByIdAsync(query.Id);
        }
        public async Task<IEnumerable<Locality>> Handle(GetLocalitiesByFkIdDistrictQuery query)
        {
            return await localityRepository.FindByFkIdDistrictAsync(query.FkIdDistrict);
        }
    }
}
