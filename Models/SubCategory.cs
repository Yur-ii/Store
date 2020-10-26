using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public string PatternСharacteristics { get; set; }
        public string Desc { get; set; }
        public List<Product> Products { get; set; }
    }
}
