﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeepTargeting.Models;
using Microsoft.AspNetCore.Mvc;
using DeepTargeting.Services;

namespace DeepTargeting.Controllers
{
    public class QueryController : Controller
    {
        private readonly IQueryService queryService;

        private static QueryViewModel viewModel = new QueryViewModel();

        public QueryController(IQueryService queryService)
        {
            this.queryService = queryService;
        }

        public IActionResult Index()
        {
            return View(viewModel);
        }

        public IActionResult FindKeywordInterests(QueryViewModel queryViewModel)
        {
            viewModel.CreatedQuery = queryViewModel.CreatedQuery;
            viewModel.FoundInterests = queryService.GetKeywordInterests(queryViewModel.CreatedQuery.QueryText);

            return RedirectToAction("Index");
        }
    }
}