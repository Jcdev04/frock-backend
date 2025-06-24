﻿using Frock_backend.routes.Interface.REST.Resources;
using Frock_backend.routes.Domain.Model.Aggregates;
namespace Frock_backend.routes.Interface.REST.Transform
{
    public class RouteAggregateResourceFromResourceAssembler
    {
        public static RouteAggregateResource ToResourceFromEntity(RouteAggregate routeAggregate) =>
            new RouteAggregateResource(
                routeAggregate.Id,
                routeAggregate.Price,
                routeAggregate.Frequency,
                routeAggregate.Duration,
                routeAggregate.Stops.Select(stop => new StopInRoutesResource(stop.Id, stop.Stop.Name, stop.Stop.Address)).ToList(),
                routeAggregate.Schedules.Select(schedule => new ScheduleResource(schedule.Id, schedule.StartTime, schedule.EndTime, schedule.DayOfWeek, schedule.Enabled)).ToList()
            );
    
    }
}
