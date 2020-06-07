using System.Threading.Tasks;
using DeepTargeting.Models;
using Microsoft.AspNetCore.Mvc;
using DeepTargeting.Services;
using Microsoft.AspNetCore.Authorization;
using DeepTargeting.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
            return View(viewModel);
        }

        public async Task<IActionResult> FindKeywordInterests(QueryViewModel queryViewModel)
        {
            viewModel.CreatedQuery = queryViewModel.CreatedQuery;
            viewModel.FoundInterests = await queryService.GetKeywordInterests(queryViewModel.CreatedQuery);

            viewModel.CreatedQuery.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await dbContext.QueryTexts.AddAsync(viewModel.CreatedQuery);
            await dbContext.SaveChangesAsync();

            viewModelCopyForExcel = (QueryViewModel)viewModel.Clone();

            return RedirectToAction("Index");
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