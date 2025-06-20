using Frock_backend.stops.Domain.Model.Commands.Geographic;
using Frock_backend.stops.Interfaces.REST.Resources.Geographic;

namespace Frock_backend.stops.Interfaces.REST.Transform.Geographic
{
    public class CreateLocalityCommandFromResourceAssembler
    {
        public static CreateLocalityCommand ToCommandFromResource(CreateLocalityResource resource) =>
            new CreateLocalityCommand(
                resource.Id,
                resource.Name,
                resource.FkIdDistrict
            );
    }
}
