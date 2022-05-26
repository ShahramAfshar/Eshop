using EStore.Application.Interfaces.Contexts;
using EStore.Application.Interfaces.FacadPatern;
using EStore.Application.Services.Products.Commands.AddNewCategory;
using EStore.Application.Services.Products.Queries.GetCategories;
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

        public ProductFasad(IDataBaseContext context)
        {
            _context = context;
        }

        private AddNewCategorService _addNewCategorService;
        public AddNewCategorService AddNewCategorService
        {
            get
            {
                return _addNewCategorService = _addNewCategorService ?? new AddNewCategorService(_context);
            }
        }

        private GetCategoriesService _getCategoriesService;
        public GetCategoriesService GetCategoriesService
        {
            get
            {
                return _getCategoriesService = _getCategoriesService ?? new GetCategoriesService(_context);
            }
        }
       
    }


}
