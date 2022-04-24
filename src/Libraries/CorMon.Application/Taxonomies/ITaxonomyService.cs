using CorMon.Application.Taxonomies.Dto;
using CorMon.Core.Enums;
using CorMon.Core.Helpers;
using CorMon.Core.JsonModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CorMon.Application.Taxonomies
{
  public  interface ITaxonomyService
    {
        Task<TaxonomyOutput> GetAsync(string id);
        Task<PublicJsonResult> CreateAsync(TaxonomyInput input);
        Task<PublicJsonResult> UpdateAsync(TaxonomyInput input);
        Task<IEnumerable<TaxonomyOutput>> SearchAsync(string term, TaxonomyType? type, SortOrder sortOrder);
        Task<SelectListItem[]> GetCategoriesSelectListAsync(string[] categoryIds=null);
    }
}
