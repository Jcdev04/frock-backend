using Frock_backend.Profiles.Domain.Model.Aggregates;
using Frock_backend.Profiles.Interfaces.REST.Resources;

namespace Frock_backend.Profiles.Interfaces.REST.Transform;

/// <summary>
/// Assembler class to convert Profile entity to ProfileResource 
/// </summary>
public static class ProfileResourceFromEntityAssembler
{
    /// <summary>
    /// Convert Profile entity to ProfileResource 
    /// </summary>
    /// <param name="entity">
    /// <see cref="Profile"/> entity to convert
    /// </param>
    /// <returns>
    /// <see cref="ProfileResource"/> converted from <see cref="Profile"/> entity
    /// </returns>
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(entity.Id, entity.FullName, entity.EmailAddress, entity.StreetAddress);
    }
}