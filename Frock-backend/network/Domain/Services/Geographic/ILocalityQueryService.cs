using Frock_backend.stops.Domain.Model.Queries.Geographic;
using Frock_backend.stops.Domain.Model.Aggregates.Geographic;

namespace Frock_backend.stops.Domain.Services.Geographic
{
    public interface ILocalityQueryService
    {
        Task<IEnumerable<Locality>> Handle(GetAllLocalitiesQuery query);

        /// <summary>
        ///     Handle the GetLocalitiesByFkIdDistrictQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetLocalitiesByFkIdDistrictQuery. It returns all the localities for the given
        ///     FkIdDistrict.
        /// </remarks>
        /// <param name="query">The GetLocalitiesByFkIdDistrictQuery query</param>
        /// <returns>An IEnumerable containing the Locality objects</returns>
        Task<IEnumerable<Locality>> Handle(GetLocalitiesByFkIdDistrictQuery query);
        /// <summary>
        ///     Handle the GetLocalityByIdQuery.
        /// </summary>
        /// <remarks>
        ///     This method handles the GetLocalityByIdQuery. It returns the locality for the given Id.
        /// </remarks>
        /// <param name="query">The GetLocalityByIdQuery query</param>
        /// <returns>
        ///     The Locality object if found, or null otherwise
        /// </returns>
        Task<Locality?> Handle(GetLocalityByIdQuery query);
    }
}
