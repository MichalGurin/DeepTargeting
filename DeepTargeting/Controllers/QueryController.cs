using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using DeepTargeting.Models;
using DeepTargeting.Data;
using DeepTargeting.Services;
using System.Linq;

namespace DeepTargeting.Controllers
{
    [Authorize]
    public class QueryController : Controller
    {
        private readonly IQueriesRepository queriesRepo;
        private readonly IQueryService queryService;
        private readonly IQueryExportService exportService;

        private static QueryViewModel viewModel;
        private static QueryViewModel viewModelCopyForExcel;

        public QueryController(IQueriesRepository queriesRepo, IQueryService queryService, IQueryExportService exportService)
        {
            this.queriesRepo = queriesRepo;
            this.queryService = queryService;
            this.exportService = exportService;

            if (viewModel == null)
            {
                viewModel = new QueryViewModel();
            }
        }

        public IActionResult Index()
        {
            List<Query> usersPreviousQueries = queriesRepo.GetQueriesOfUser(User); 

            foreach (Query query in usersPreviousQueries)
            {
                viewModel.PreviousQueries.Add(query.QueryText);
            }

            viewModel.PreviousQueries = viewModel.PreviousQueries.Distinct().ToList();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FindKeywordInterests(QueryViewModel queryViewModel)
        {
            viewModel.CreatedQuery = queryViewModel.CreatedQuery;
            viewModel.FoundInterests = await queryService.GetKeywordInterests(queryViewModel.CreatedQuery);
            viewModel.CreatedQuery.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (queriesRepo.QueryNotInDB(viewModel.CreatedQuery))
            {
                await queriesRepo.AddQueryToDBAsync(viewModel.CreatedQuery);
            }

            viewModelCopyForExcel = (QueryViewModel)viewModel.Clone();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ReloadPageWithQuery(string queryText)
        {
            if (queryText.Length == 0)
            {
                return BadRequest();
            }

            viewModel.CreatedQuery = new Query(queryText, "en_us");
            viewModel.FoundInterests = await queryService.GetKeywordInterests(viewModel.CreatedQuery);
            viewModel.CreatedQuery.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (queriesRepo.QueryNotInDB(viewModel.CreatedQuery))
            {
                await queriesRepo.AddQueryToDBAsync(viewModel.CreatedQuery);
            }

            viewModelCopyForExcel = (QueryViewModel)viewModel.Clone();

            string redirectUrl = Url.Action("Index");
            return Json(new { redirectUrl });
        }

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