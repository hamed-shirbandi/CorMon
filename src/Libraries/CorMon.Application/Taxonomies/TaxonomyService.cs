using CorMon.Application.Taxonomies.Dto;
using CorMon.Core.JsonModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CorMon.Core.Enums;
using CorMon.Core.Data;
using CorMon.Resource;
using CorMon.Core.Domain;
using System.Linq;
using CorMon.Core.Extensions;
using CorMon.Core.Helpers;
using CorMon.Application.Mapper;

namespace CorMon.Application.Taxonomies
{
    public class TaxonomyService : ITaxonomyService
    {
        #region Fields

        private readonly ITaxonomyRepository _taxonomyRepository;
        private readonly IMapperService _mapperService;


        #endregion

        #region Ctor

        public TaxonomyService(ITaxonomyRepository taxonomyRepository, IMapperService mapperService)
        {
            _taxonomyRepository = taxonomyRepository;
            _mapperService = mapperService;
        }


        #endregion

        #region Public Methods





        /// <summary>
        /// 
        /// </summary>
        public async Task<TaxonomyOutput> GetAsync(string id)
        {
            var taxonomy = await _taxonomyRepository.GetByIdAsync(id);
            if (taxonomy == null)
            {
                throw new Exception("Taxonomy not found");
            }


            return _mapperService.BindToOutputModel(taxonomy);
        }





        /// <summary>
        /// 
        /// </summary>
        public async Task<PublicJsonResult> CreateAsync(TaxonomyInput input)
        {

            //بررسی یکتا بودن عنوان 
            var existTax = await _taxonomyRepository.GetByNameAsync(input.Name.Trim());
            if (existTax != null)
                return new PublicJsonResult { Result = false, Message = Messages.Post_Title_Already_Exist };

            //بررسی نامک -- url friendly
            input.UrlTitle = input.UrlTitle.IsNullOrEmptyOrWhiteSpace() ? input.Name.GenerateUrlTitle() : input.UrlTitle.GenerateUrlTitle();

            var taxonomy = _mapperService.BindToDomainModel(input);

            await _taxonomyRepository.CreateAsync(taxonomy);
            return new PublicJsonResult { Result = true, Id = taxonomy.Id, Message = Messages.Post_Create_Success };
        }






        /// <summary>
        /// 
        /// </summary>
        public async Task<PublicJsonResult> UpdateAsync(TaxonomyInput input)
        {
            var tax = await _taxonomyRepository.GetByIdAsync(input.Id);
            if (tax == null)
            {
                throw new Exception("Taxonomy not found");
            }

            //بررسی یکتا بودن عنوان 
            var existTax = await _taxonomyRepository.GetByNameAsync(input.Name.Trim());
            if (existTax != null && existTax.Id != input.Id)
                return new PublicJsonResult { Result = false, Message = Messages.Post_Title_Already_Exist };


            //بررسی نامک -- url friendly
            tax.UrlTitle = input.UrlTitle.IsNullOrEmptyOrWhiteSpace() ? input.Name.GenerateUrlTitle() : input.UrlTitle.GenerateUrlTitle();
            tax.Name = input.Name;
            tax.Description = input.Name;


            await _taxonomyRepository.UpdateAsync(tax);
            return new PublicJsonResult { Result = true, Message = Messages.Post_Update_Success };


        }






        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<TaxonomyOutput>> SearchAsync(string term, TaxonomyType? type, SortOrder sortOrder)
        {
            var taxonomies = await _taxonomyRepository.SearchAsync(term, type, sortOrder);

            return taxonomies.Select(taxonomy => _mapperService.BindToOutputModel(taxonomy)).ToList();
        }
        




        /// <summary>
        /// 
        /// </summary>
        public async Task<SelectListItem[]> GetCategoriesSelectListAsync(string[] categoryIds = null)
        {
            if (categoryIds==null)
                categoryIds = new string[] { };

            var categories = await _taxonomyRepository.GetAllAsync(TaxonomyType.Category);
            return categories.Select(t=> new SelectListItem
            {
                Selected= categoryIds.Contains(t.Id),
                Text=t.Name,
                Value=t.Id

            }).ToArray();
        }







        #endregion

    }
}
