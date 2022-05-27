using EStore.Domain.Entities.Commons;

namespace EStore.Domain.Entities.Products
{
    public class ProductImage:BaseEntity
    {
        public string SourceImage { get; set; }

        public virtual Product  Product { get; set; }
        public int ProductId { get; set; }
    }

}
