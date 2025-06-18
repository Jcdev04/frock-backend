namespace Frock_backend.stops.Interfaces.REST.Resources.Geographic
{
    public record CreateDistrictResource(
        string Id,
        string Name,
        string FkIdProvince
        );
}
