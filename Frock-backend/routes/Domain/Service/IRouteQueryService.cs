using Frock_backend.routes.Domain.Model.Queries;
using Frock_backend.routes.Domain.Model.Aggregates;
namespace Frock_backend.routes.Domain.Service
{
    public interface IRouteQueryService
    {
        Task<IEnumerable<RouteAggregate>> Handle(GetAllRoutesByFkCompanyIdQuery query);
    }
}
