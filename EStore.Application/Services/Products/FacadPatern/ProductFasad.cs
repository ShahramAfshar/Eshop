using EStore.Application.Interfaces.Contexts;
using EStore.Application.Interfaces.FacadPatern;
using EStore.Application.Services.Products.Commands.AddNewCategory;
using EStore.Application.Services.Products.Commands.AddNewProduct;
using EStore.Application.Services.Products.Commands.IncreaseViewProduct;
using EStore.Application.Services.Products.Queries.GetAllCategories;
using EStore.Application.Services.Products.Queries.GetCategories;
using EStore.Application.Services.Products.Queries.GetProductDetailForAdmin;
using EStore.Application.Services.Products.Queries.GetProductDetailForSite;
using EStore.Application.Services.Products.Queries.GetProductForAdmin;
using EStore.Application.Services.Products.Queries.GetProductForSite;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Products.FacadPatern
{
    public class ProductFasad : IProductFasad
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
     //   private readonly IIncreaseViewProductService _increaseViewProductService;

        public ProductFasad(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
          //  _increaseViewProductService = increaseViewProductService;
        }

        private IAddNewCategoryService _addNewCategorService;
        public IAddNewCategoryService AddNewCategorService
        {
            get
            {
                return _addNewCategorService = _addNewCategorService ?? new AddNewCategorService(_context);
            }
        }

        private IGetCategoriesService _getCategoriesService;
        public IGetCategoriesService GetCategoriesService
        {
            get
            {
                return _getCategoriesService = _getCategoriesService ?? new GetCategoriesService(_context);
            }
        }

        private IAddNewProductService _addNewProductService;

        public IAddNewProductService AddNewProductService
        {
            get
            {
                return _addNewProductService = _addNewProductService ?? new AddNewProductService(_context, _environment);
            }
        }

        private IGetAllCategoriesService _getAllCategoriesService;
        public IGetAllCategoriesService GetAllCategoriesService
        {
            get
            {
                return _getAllCategoriesService = _getAllCategoriesService ?? new GetAllCategoriesService(_context);
            }
        }

        private IGetProductForAdminService _getProductForAdminService;
        public IGetProductForAdminService GetProductForAdminService
        {
            get
            {
                return _getProductForAdminService = _getProductForAdminService ?? new GetProductForAdminService(_context);
            }
        }

        private IGetProductDetailForAdminService _getProductDetailForAdminService;
        public IGetProductDetailForAdminService GetProductDetailForAdminService
        {
            get
            {
                return _getProductDetailForAdminService = _getProductDetailForAdminService ?? new GetProductDetailForAdminService(_context);
            }
        }

        private IGetProductForSiteService _getProductForSiteService;
        public IGetProductForSiteService GetProductForSiteService
        {
            get
            {

                return _getProductForSiteService = _getProductForSiteService ?? new GetProductForSiteService(_context);
            }
        }

        private IGetProductDetailForSiteService _getProductDetailForSiteService;
        public IGetProductDetailForSiteService GetProductDetailForSiteService
        {
            get
            {
                return _getProductDetailForSiteService = _getProductDetailForSiteService ?? new GetProductDetailForSiteService(_context);
            }
        }

        //private IIncreaseViewProductService _increaseViewProductServiceBae;
        //public IIncreaseViewProductService IncreaseViewProductService
        //{
        //    get
        //    {
        //        return _increaseViewProductServiceBae = _increaseViewProductServiceBae ?? new IncreaseViewProductService(_context);
        //    }
        //}
    }


}
