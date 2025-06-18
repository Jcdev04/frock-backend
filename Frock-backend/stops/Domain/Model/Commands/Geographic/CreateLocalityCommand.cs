namespace Frock_backend.stops.Domain.Model.Commands.Geographic
{
    public record CreateLocalityCommand(string Id, string Name, string FkIdDistrict);
}
