namespace Frock_backend.stops.Domain.Model.Commands.Geographic
{
    public record CreateDistrictCommand(string Id, string Name, string FkIdProvince);
}
