using Frock_backend.stops.Domain.Model.Aggregates.Geographic;
using Frock_backend.stops.Interfaces.REST.Resources.Geographic;

namespace Frock_backend.stops.Interfaces.REST.Transform.Geographic
{
    public static class LocalityResourceFromEntityAssembler
    {
        public static LocalityResource ToResourceFromEntity(Locality entity) =>
            new LocalityResource(
                entity.Id,
                entity.Name,
                entity.FkIdDistrict
            );
    }
}
