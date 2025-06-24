namespace Frock_backend.stops.Domain.Model.Commands.Geographic
{
    public record CreateProvinceCommand(string Id, string Name, string FkIdRegion);
}
