﻿using EStore.Application.Services.Common.Queries.GetCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.ViewComponents
{
    public class Search:ViewComponent
    {
        private readonly IGetCategoryService _getCategoryService;

        public Search(IGetCategoryService getCategoryService)
        {
            _getCategoryService = getCategoryService;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _getCategoryService.Execute();
            return View(viewName:"Search",model:categories.Data);
        }


    }
}
