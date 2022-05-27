using EStore.Application.Services.Products.Commands.AddNewCategory;
using EStore.Application.Services.Products.Commands.AddNewProduct;
using EStore.Application.Services.Products.Queries.GetAllCategories;
using EStore.Application.Services.Products.Queries.GetCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces.FacadPatern
{
    public interface IProductFasad
    {
        AddNewCategorService AddNewCategorService { get; }
        GetCategoriesService GetCategoriesService { get; }

        AddNewProductService AddNewProductService { get; }

        GetAllCategoriesService GetAllCategoriesService { get; }
    }
}
