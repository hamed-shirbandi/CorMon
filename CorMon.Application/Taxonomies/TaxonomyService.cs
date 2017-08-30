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

namespace CorMon.Application.Taxonomies
{
    public class TaxonomyService : ITaxonomyService
    {
        #region Fields

        private readonly ITaxonomyRepository _taxonomyRepository;


        #endregion

        #region Ctor

        public TaxonomyService(ITaxonomyRepository taxonomyRepository)
        {
            _taxonomyRepository = taxonomyRepository;
        }


        #endregion

        #region Methods





        /// <summary>
        /// 
        /// </summary>
        public async Task<TaxonomyInput> GetAsync(string id)
        {
            var tax = await _taxonomyRepository.GetByIdAsync(id);
            if (tax == null)
            {
                throw new Exception("Taxonomy not found");
            }


            return new TaxonomyInput
            {
                Id = tax.Id,
                Name = tax.Name,
                Description = tax.Description,
                PostCount = tax.PostCount,
                Type = tax.Type,
                UrlTitle = tax.UrlTitle,
            };
        }





        /// <summary>
        /// 
        /// </summary>
        public async Task<PostJsonResult> CreateAsync(TaxonomyInput input)
        {

            //بررسی یکتا بودن عنوان 
            var existTax = await _taxonomyRepository.GetByNameAsync(input.Name.Trim());
            if (existTax != null)
                return new PostJsonResult { result = false, message = Messages.Post_Title_Already_Exist };

            //بررسی نامک -- url friendly
            input.UrlTitle = input.UrlTitle.IsNullOrEmptyOrWhiteSpace() ? input.Name.GenerateUrlTitle() : input.UrlTitle.GenerateUrlTitle();

            var post = new Taxonomy
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                PostCount = input.PostCount,
                Type = input.Type,
                UrlTitle = input.UrlTitle,
            };

            await _taxonomyRepository.CreateAsync(post);
            return new PostJsonResult { result = true, id = post.Id, message = Messages.Post_Create_Success };
        }






        /// <summary>
        /// 
        /// </summary>
        public async Task<PostJsonResult> UpdateAsync(TaxonomyInput input)
        {
            var tax = await _taxonomyRepository.GetByIdAsync(input.Id);
            if (tax == null)
            {
                throw new Exception("Taxonomy not found");
            }

            //بررسی یکتا بودن عنوان 
            var existTax = await _taxonomyRepository.GetByNameAsync(input.Name.Trim());
            if (existTax != null && existTax.Id != input.Id)
                return new PostJsonResult { result = false, message = Messages.Post_Title_Already_Exist };


            //بررسی نامک -- url friendly
            tax.UrlTitle = input.UrlTitle.IsNullOrEmptyOrWhiteSpace() ? input.Name.GenerateUrlTitle() : input.UrlTitle.GenerateUrlTitle();
            tax.Name = input.Name;
            tax.Description = input.Name;


            await _taxonomyRepository.UpdateAsync(tax);
            return new PostJsonResult { result = true, message = Messages.Post_Update_Success };


        }






        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<TaxonomyInput>> SearchAsync(string term, TaxonomyType? type, SortOrder sortOrder)
        {
            var taxs = await _taxonomyRepository.SearchAsync(term, type, sortOrder);
            return taxs.Select(tax =>
            new TaxonomyInput
            {
                Id = tax.Id,
                Name = tax.Name,
                Description = tax.Description,
                PostCount = tax.PostCount,
                Type = tax.Type,
                UrlTitle = tax.UrlTitle,

            }).ToList();
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
