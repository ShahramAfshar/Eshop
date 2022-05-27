using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EStore.Application.Services.Products.Commands.AddNewProduct
{
    public class RequstAddNewProductDto
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool IsDisplay { get; set; }
        public int CategoryId { get; set; }

        public List<IFormFile> Images { get; set; }
        public List<AddNewProductFeature> Feature { get; set; }

    }




}
