﻿using Frock_backend.routes.Domain.Model.Aggregates;
using Frock_backend.routes.Domain.Repository;
using Frock_backend.shared.Domain.Repositories;
using Frock_backend.shared.Infrastructure.Persistences.EFC.Configuration;
using Frock_backend.shared.Infrastructure.Persistences.EFC.Repositories;
using Frock_backend.stops.Domain.Model.Aggregates;
using Frock_backend.stops.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Frock_backend.routes.Infrastructure.Repositories
{
    public class RouteRepository(AppDbContext context) : BaseRepository<RouteAggregate>(context), IRouteRepository
    {
        public Task<List<RouteAggregate>> FindByCompanyId(int companyId)
        {
            return Context.Set<RouteAggregate>()
            // Incluimos Stops → Stop para poder filtrar por FkIdCompany
            .Include(r => r.Stops)
                .ThenInclude(rs => rs.Stop)
            .Include(r => r.Schedules)
            .Where(r => r.Stops.Any(rs => rs.Stop.FkIdCompany == companyId))
            .ToListAsync();
        }
    }
}
