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
        public string Desc { get; set; }
        public byte[] Img { get; set; }
        public double Price { get; set; }
        public bool IsFavorite { get; set; }
        public int Avilable { get; set; }
        public int SubCategoryId { get; set; }
        public string UserId { get; set; }
        public string Characteristic { get; set; }
        public SubCategory SubCategory { get; set; }
        public User Users{ get; set; }
    }
}
