using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DeepTargeting.Models;

namespace DeepTargeting.Services
{
    public interface IQueryService
    {
        public Task<List<Interest>> GetKeywordInterests(Query query);

        //public Task<List<Interest>> GetKeywordInterests(string queryText);
    }
}
