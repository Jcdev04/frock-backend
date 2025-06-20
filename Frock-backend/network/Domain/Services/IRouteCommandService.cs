using Frock_backend.network.Domain.Model.Commands;

namespace Frock_backend.network.Domain.Services
{
    public interface IRouteCommandService
    {
        Task<Route?> Handle(CreateRouteCommand command);
    }
}
