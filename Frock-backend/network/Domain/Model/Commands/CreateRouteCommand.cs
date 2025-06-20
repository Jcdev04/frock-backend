using ZstdSharp.Unsafe;

namespace Frock_backend.network.Domain.Model.Commands
{
    public record CreateRouteCommand(
        double Price, // The price of the route
        int Duration, // The duration of the route in minutes
        int Frequency // The frequency of the route in minutes, e.g., how often the route runs
    );
}
