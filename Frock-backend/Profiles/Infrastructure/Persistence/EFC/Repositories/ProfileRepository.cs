using Frock_backend.Profiles.Domain.Model.Aggregates;
using Frock_backend.Profiles.Domain.Model.ValueObjects;
using Frock_backend.Profiles.Domain.Repositories;
using Frock_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Frock_backend.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Frock_backend.Profiles.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Profile repository implementation  
/// </summary>
/// <param name="context">
/// The database context
/// </param>
public class ProfileRepository(AppDbContext context) 
    : BaseRepository<Profile>(context), IProfileRepository
{
    /// <inheritdoc />
    public async Task<Profile?> FindProfileByEmailAsync(EmailAddress email)
    {
        return Context.Set<Profile>().FirstOrDefault(p => p.Email == email);
    }
}