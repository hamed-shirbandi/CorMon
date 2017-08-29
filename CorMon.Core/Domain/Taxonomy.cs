using CorMon.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorMon.Core.Domain
{
   public class Taxonomy :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PostCount { get; set; }
        public TaxonomyType Type { get; set; }
    }
}
