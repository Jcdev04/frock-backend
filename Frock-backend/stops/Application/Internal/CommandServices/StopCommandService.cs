using Frock_backend.shared.Domain.Repositories;
using Frock_backend.stops.Domain.Model.Aggregates;
using Frock_backend.stops.Domain.Model.Commands;
using Frock_backend.stops.Domain.Repositories;
using Frock_backend.stops.Domain.Services;

namespace Frock_backend.stops.Application.Internal.CommandServices
{
    /// <summary>
    ///     Stop command service.
    /// </summary>
    /// <remarks>
    ///     This class implements the basic operations for a Stop command service.
    /// </remarks>
    /// <param name="stopRepository">The instance of stopRepository</param>
    /// <param name="unitOfWork">The instance of UnitOfWork</param>
    /// See
    /// <see cref="IStopRepository">IStopRepository</see>
    /// ,
    /// <see cref="IUnitOfWork">IUnitOfWork</see>
    public class StopCommandService(IStopRepository stopRepository, IUnitOfWork unitOfWork) : IStopCommandService
    {
        public async Task<Stop?> Handle(CreateStopCommand command)
        {
            var stop =
                await stopRepository.FindByNameAndFkIdLocalityAsync(command.Name, command.FkIdLocality);
            if (stop != null) throw new Exception("Stop with this name already exists for the given FkIdLocality");
            stop = new Stop(command);
            try
            {
                await stopRepository.AddAsync(stop);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return null;
            }

            return stop;
        }



    }
}
