using Frock_backend.Profiles.Domain.Model.Aggregates;
using Frock_backend.Profiles.Domain.Model.Commands;
using Frock_backend.Profiles.Domain.Repositories;
using Frock_backend.Profiles.Domain.Services;
using Frock_backend.Shared.Domain.Repositories;

namespace Frock_backend.Profiles.Application.Internal.CommandServices;

/// <summary>
/// Profile command service 
/// </summary>
/// <param name="profileRepository">
/// Profile repository
/// </param>
/// <param name="unitOfWork">
/// Unit of work
/// </param>
public class ProfileCommandService(
    IProfileRepository profileRepository, 
    IUnitOfWork unitOfWork) 
    : IProfileCommandService
{
    /// <inheritdoc />
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);
        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        } catch (Exception e)
        {
            // Log error
            return null;
        }
    }
}