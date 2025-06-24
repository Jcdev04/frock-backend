﻿using Frock_backend.routes.Domain.Model.Aggregates;
using Frock_backend.shared.Domain.Repositories;

namespace Frock_backend.routes.Domain.Repository
{
    public interface IRouteRepository:IBaseRepository<RouteAggregate>
    {
        Task<List<RouteAggregate>> FindByCompanyId(int companyId);
    }
}
