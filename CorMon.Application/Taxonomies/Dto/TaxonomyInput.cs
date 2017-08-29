using CorMon.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Application.Taxonomies.Dto
{
   public class TaxonomyInput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PostCount { get; set; }
        public TaxonomyType Type { get; set; }
    }
}
