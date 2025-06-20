using Frock_backend.Profiles.Domain.Model.Aggregates;
using Frock_backend.Profiles.Domain.Model.Queries;
using Frock_backend.Profiles.Domain.Repositories;
using Frock_backend.Profiles.Domain.Services;

namespace Frock_backend.Profiles.Application.Internal.QueryServices;

/// <summary>
/// Profile query service 
/// </summary>
/// <param name="profileRepository">
/// Profile repository
/// </param>
public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }

    /// <inheritdoc />
    public async Task<Profile?> Handle(GetProfileByEmailQuery query)
    {
        return await profileRepository.FindProfileByEmailAsync(query.Email);
    }

    /// <inheritdoc />
    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.ProfileId);
    }
}