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
        Task<TaxonomyInput> GetAsync(string id);
        Task<PostJsonResult> CreateAsync(TaxonomyInput input);
        Task<PostJsonResult> UpdateAsync(TaxonomyInput input);
        Task<IEnumerable<TaxonomyInput>> SearchAsync(string term, TaxonomyType? type, SortOrder sortOrder);
        Task<SelectListItem[]> GetCategoriesSelectListAsync(string[] categoryIds=null);
    }
}
