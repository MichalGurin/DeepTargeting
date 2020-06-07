using DeepTargeting.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepTargeting.Services
{
    public interface IQueryExportService
    {
        public byte[] Export(QueryViewModel queryModel);
    }
}
