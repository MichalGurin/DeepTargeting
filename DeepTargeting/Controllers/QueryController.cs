using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using DeepTargeting.Models;
using DeepTargeting.Data;
using DeepTargeting.Services;
using System.Linq;

namespace DeepTargeting.Controllers
{
    [Authorize]
    public class QueryController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IQueryService queryService;
        private readonly IQueryExportService exportService;

        private static QueryViewModel viewModel = new QueryViewModel();
        private static QueryViewModel viewModelCopyForExcel = new QueryViewModel();

        public QueryController(ApplicationDbContext dbContext, IQueryService queryService, IQueryExportService exportService)
        {
            this.dbContext = dbContext;
            this.queryService = queryService;
            this.exportService = exportService;
        }

        public IActionResult Index()
        {
            List<Query> usersPreviousQueries = dbContext.AllQueries.Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();

            foreach (Query item in usersPreviousQueries)
            {
                viewModel.PreviousQueries.Add(item.QueryText);
            }

            viewModel.PreviousQueries = viewModel.PreviousQueries.Distinct().ToList();
            return View(viewModel);
        }

        public async Task<IActionResult> FindKeywordInterests(QueryViewModel queryViewModel)
        {
            viewModel.CreatedQuery = queryViewModel.CreatedQuery;
            viewModel.FoundInterests = await queryService.GetKeywordInterests(queryViewModel.CreatedQuery);

            viewModel.CreatedQuery.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await dbContext.AllQueries.AddAsync(viewModel.CreatedQuery);
            await dbContext.SaveChangesAsync();

            viewModelCopyForExcel = (QueryViewModel)viewModel.Clone();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public void ReloadPageWithQuery(string queryText)
        {
            viewModel = new QueryViewModel();
            viewModel.CreatedQuery = new Query();
            viewModel.CreatedQuery.QueryText = queryText;
            RedirectToAction("Index");
        }

        [HttpPost]
        public void ResetQuery()
        {
            viewModel = new QueryViewModel();
        }

        public IActionResult ExportExcel()
        {
            return File(exportService.Export(viewModelCopyForExcel),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        viewModelCopyForExcel.CreatedQuery.QueryText + "Export.xlsx");
        }
    }
}