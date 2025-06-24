using Frock_backend.routes.Domain.Model.Aggregates;
using Frock_backend.routes.Domain.Model.Queries;
using Frock_backend.routes.Domain.Repository;
using Frock_backend.routes.Domain.Service;
using Frock_backend.shared.Infrastructure.Persistences.EFC.Repositories;
namespace Frock_backend.routes.Application.Internal.QueryServices
{
    public class RouteQueryService(IRouteRepository routeRepository) : IRouteQueryService
    {
        public async Task<IEnumerable<RouteAggregate>> Handle(GetAllRoutesByFkCompanyIdQuery query)
        {
            try
            {
                return await routeRepository.FindByCompanyId(query.FkCompanyId);
            }
            catch (Exception e)
            {

                throw new Exception($"Error retrieving routes for company: {e.Message}", e);
            }
        }
    }
}
