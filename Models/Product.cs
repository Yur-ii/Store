using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LongDesc { get; set; }
        public string ShortDesc { get; set; }
        public string Img { get; set; }
        public double Prive { get; set; }
        public bool IsFavorite { get; set; }
        public int Avilable { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
