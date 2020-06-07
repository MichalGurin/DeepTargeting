using System.Threading.Tasks;
using DeepTargeting.Models;
using Microsoft.AspNetCore.Mvc;
using DeepTargeting.Services;
using Microsoft.AspNetCore.Authorization;
using ClosedXML.Excel;
using System.IO;

namespace DeepTargeting.Controllers
{
    [Authorize]
    public class QueryController : Controller
    {
        private readonly IQueryService queryService;

        private readonly IQueryExportService exportService;

        private static QueryViewModel viewModel = new QueryViewModel();

        private static QueryViewModel viewModelCopyForExcel = new QueryViewModel();

        public QueryController(IQueryService queryService, IQueryExportService exportService)
        {
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