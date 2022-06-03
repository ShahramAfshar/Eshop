using System.Collections.Generic;

namespace EStore.Application.Services.Products.Queries.GetProductDetailForAdmin
{
    public class ProductDetailForAdminDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool IsDisplay { get; set; }
        public List<ProductDetailImageDto> Images { get; set; }
        public List<ProductDetailFeatureDto> Feature { get; set; }

    }

}
