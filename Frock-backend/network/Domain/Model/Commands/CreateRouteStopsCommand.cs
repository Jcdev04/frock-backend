namespace Frock_backend.network.Domain.Model.Commands
{
    public record CreateRouteStopsCommand(
        int FkIdRoute, // This is a foreign key to a Route entity
        int FkIdStop // This is a foreign key to a Stop entity
    );
      
}
