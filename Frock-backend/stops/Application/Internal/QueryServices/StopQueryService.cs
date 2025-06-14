using Frock_backend.stops.Domain.Model.Aggregates;
using Frock_backend.stops.Domain.Model.Queries;
using Frock_backend.stops.Domain.Repositories;
using Frock_backend.stops.Domain.Services;

namespace Frock_backend.stops.Application.Internal.QueryServices
{
    public class StopQueryService(IStopRepository stopRepository) : IStopQueryService
    {

        public async Task<IEnumerable<Stop>> Handle(GetAllStopsByFkIdCompanyQuery query)
        {
            return await stopRepository.FindByFkIdCompanyAsync(query.FkIdCompany);
        }
        public async Task<IEnumerable<Stop>> Handle(GetAllStopsByFkIdLocalityQuery query)
        {
            return await stopRepository.FindByFkIdLocalityAsync(query.FkIdLocality);
        }
        public async Task<Stop?> Handle(GetStopByIdQuery query)
        {
            return await stopRepository.FindByIdAsync(query.Id);
        }
        public async Task<Stop?> Handle(GetStopByNameAndFkIdLocalityQuery query)
        {
            return await stopRepository.FindByNameAndFkIdLocalityAsync(query.Name, query.FkIdLocality);
        }

    }
}
