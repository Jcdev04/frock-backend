using Frock_backend.stops.Domain.Model.Commands.Geographic;
using Frock_backend.stops.Domain.Model.Aggregates.Geographic;

namespace Frock_backend.stops.Domain.Services.Geographic
{
    public interface ILocalityCommandService
    {
        /// <summary>
        ///     Handle the create locality command.
        /// </summary>
        /// <remarks>
        ///     This method handles the create locality command. It checks if the locality already exists for the
        ///     given parameters. If it exists, it updates the existing locality with the new values.
        ///     If it does not exist, it creates a new locality and adds it to the database.
        /// </remarks>
        /// <param name="command">CreateLocalityCommand command</param>
        /// <returns></returns>
        Task<Locality?> Handle(CreateLocalityCommand command);
    }
}
