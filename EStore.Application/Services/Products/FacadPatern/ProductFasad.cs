using EStore.Application.Interfaces.Contexts;
using EStore.Application.Interfaces.FacadPatern;
using EStore.Application.Services.Products.Commands.AddNewCategory;
using EStore.Application.Services.Products.Commands.AddNewProduct;
using EStore.Application.Services.Products.Queries.GetAllCategories;
using EStore.Application.Services.Products.Queries.GetCategories;
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

        public ProductFasad(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private AddNewCategorService _addNewCategorService;
        public AddNewCategorService AddNewCategorService
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

        private AddNewProductService _addNewProductService;

        public AddNewProductService AddNewProductService
        {
            get
            {
                return _addNewProductService = _addNewProductService ?? new AddNewProductService(_context,_environment);
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
    }


}
