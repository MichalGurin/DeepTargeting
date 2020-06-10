
using DeepTargeting.Models;


namespace DeepTargeting.Services
{
    public interface IQueryExportService
    {
        public byte[] Export(QueryViewModel queryModel);
    }
}
