using EStore.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Products
{
    public class Product: BaseEntity
    {
        public string  Name { get; set; }
        public string Brand { get; set; }
        public string  Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool IsDisplay { get; set; }

        //Relation one-many 
        public  virtual Category Category  { get; set; }
        public int CategoryId { get; set; }

        //Relation one-many
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }

    }

}
