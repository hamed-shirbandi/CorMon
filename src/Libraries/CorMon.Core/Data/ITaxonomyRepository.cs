

using CorMon.Core.Domain;
using CorMon.Core.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorMon.Core.Data
{
    public interface ITaxonomyRepository
    {
        Task<Taxonomy> GetByIdAsync(string id);
        Taxonomy GetById(string id);
        Task<Taxonomy> GetByNameAsync(string name);
        Task<IEnumerable<Taxonomy>> GetAllAsync(TaxonomyType type);
        Task<IEnumerable<Taxonomy>> GetListByIdsAsync(string[] taxIds);
        IEnumerable<Taxonomy> GetListByIds(string[] taxIds);
        Task<IEnumerable<Taxonomy>> SearchAsync(string term, TaxonomyType? type, SortOrder sortOrder);
        Task CreateAsync(Taxonomy tax);
        Task CreateAsync(IEnumerable<Taxonomy> taxs);
        Task UpdateAsync(Taxonomy tax);
        Task UpdateAsync(IEnumerable<Taxonomy> taxs);
    }
}
