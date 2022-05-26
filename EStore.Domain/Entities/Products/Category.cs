using EStore.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.Entities.Products
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public int? ParentCaregoryId { get; set; }

        public virtual Category ParentCategory { get; set; }

        //برای نمایش زیر دسته
        public virtual ICollection<Category> SubCategory { get; set; }
    }
}
