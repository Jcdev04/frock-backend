using Frock_backend.shared.Domain.Repositories;
using Frock_backend.stops.Domain.Model.Aggregates.Geographic;
using Frock_backend.stops.Domain.Model.Commands.Geographic;
using Frock_backend.stops.Domain.Repositories.Geographic;
using Frock_backend.stops.Domain.Services.Geographic;

namespace Frock_backend.stops.Application.Internal.CommandServices.Geographic
{
    public class LocalityCommandService(ILocalityRepository localityRepository, IUnitOfWork unitOfWork) : ILocalityCommandService
    {
        public async Task<Locality?> Handle(CreateLocalityCommand command)
        {
            var existingLocality = await localityRepository.FindByIdAsync(command.Id);
            if (existingLocality != null)
            {
                throw new Exception($"Locality already exists with that Id.");
            }
            var newLocality = new Locality(command);
            try
            {
                await localityRepository.AddAsync(newLocality);
                await unitOfWork.CompleteAsync();
                return newLocality;
            }
            catch (Exception e)
            {
                // logger?.LogError(e, "Error creating locality with name {LocalityName}.", command.Name);
                return null; // Signal failure to the controller
            }
        }
    }
}
