using CorMon.Core.Enums;
using CorMon.Resource;
using System.ComponentModel.DataAnnotations;

namespace CorMon.Application.Taxonomies.Dto
{
   public class TaxonomyOutput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PostCount { get; set; }
        public TaxonomyType Type { get; set; }

        [Display(Name = "Post_UrlTitle", ResourceType = typeof(Metadata))]
        public string UrlTitle { get; set; }

    }
}
