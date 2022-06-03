using EStore.Application.Services.Products.Commands.AddNewCategory;
using EStore.Application.Services.Products.Commands.AddNewProduct;
using EStore.Application.Services.Products.Commands.IncreaseViewProduct;
using EStore.Application.Services.Products.Queries.GetAllCategories;
using EStore.Application.Services.Products.Queries.GetCategories;
using EStore.Application.Services.Products.Queries.GetProductDetailForAdmin;
using EStore.Application.Services.Products.Queries.GetProductDetailForSite;
using EStore.Application.Services.Products.Queries.GetProductForAdmin;
using EStore.Application.Services.Products.Queries.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Interfaces.FacadPatern
{
    public interface IProductFasad
    {
        IAddNewCategoryService AddNewCategorService { get; }
        IGetCategoriesService GetCategoriesService { get; }
        IAddNewProductService AddNewProductService { get; }
        IGetAllCategoriesService GetAllCategoriesService { get; }
        IGetProductForAdminService GetProductForAdminService { get; }
        IGetProductDetailForAdminService GetProductDetailForAdminService { get; }
        IGetProductForSiteService GetProductForSiteService { get; }
        IGetProductDetailForSiteService GetProductDetailForSiteService { get; }
     //   IIncreaseViewProductService IncreaseViewProductService { get; }
    }
}
