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

        private static QueryViewModel viewModel = new QueryViewModel();

        private static QueryViewModel viewModelCopyForExcel = new QueryViewModel();

        public QueryController(IQueryService queryService)
        {
            this.queryService = queryService;
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
            using (XLWorkbook workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add(viewModelCopyForExcel.CreatedQuery.QueryText);
                int currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Name";
                worksheet.Cell(currentRow, 2).Value = "Audience Size";
                worksheet.Cell(currentRow, 3).Value = "Category";
                foreach (Interest interest in viewModelCopyForExcel.FoundInterests)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = interest.Name;
                    worksheet.Cell(currentRow, 2).Value = interest.AudienceSize;
                    worksheet.Cell(currentRow, 3).Value = interest.Category;
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    byte[] byteContent = stream.ToArray();

                    FileContentResult fileContent = File(
                        byteContent,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "KeywordExport.xlsx");

                    return fileContent;
                }
            }
        }
    }
}