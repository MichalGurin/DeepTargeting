using ClosedXML.Excel;
using DeepTargeting.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DeepTargeting.Services
{
    public class ExcelExporter : IQueryExportService
    {
        byte[] IQueryExportService.Export(QueryViewModel queryModel)
        {
            byte[] fileByteContent = null;
            using (XLWorkbook workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add(queryModel.CreatedQuery.QueryText);
                int currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Name";
                worksheet.Cell(currentRow, 2).Value = "Audience Size";
                worksheet.Cell(currentRow, 3).Value = "Category";
                foreach (Interest interest in queryModel.FoundInterests)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = interest.Name;
                    worksheet.Cell(currentRow, 2).Value = interest.AudienceSize;
                    worksheet.Cell(currentRow, 3).Value = interest.Category;
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    fileByteContent = stream.ToArray();
                }
            }
            return fileByteContent;
        }
    }
}
