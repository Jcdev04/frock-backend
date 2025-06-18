namespace Frock_backend.stops.Interfaces.REST.Resources.Geographic
{
    public record DistrictResource(
        string Id,
        string Name,
        string FkIdProvince // Foreign key to the Province entity (e.g., "prov-1" for the province this district belongs to)
        );
}
