using Frock_backend.shared.Domain.Repositories;
using Frock_backend.stops.Domain.Model.Aggregates;

namespace Frock_backend.stops.Domain.Repositories
{
    public interface IStopRepository : IBaseRepository<Stop>
    {
        Task<IEnumerable<Stop>> FindByFkIdCompanyAsync(string fkIdCompany);
        Task<IEnumerable<Stop>> FindByFkIdLocalityAsync(string fkIdLocality);
        Task<Stop?> FindByNameAndFkIdLocalityAsync(string name, string fkIdLocality);
    }
}
