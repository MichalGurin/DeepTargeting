using DeepTargeting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepTargeting.Services
{
    public interface IQueryService
    {
        public Task<List<Interest>> GetKeywordInterests(Query query);
    }
}
