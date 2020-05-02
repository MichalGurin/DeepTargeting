using DeepTargeting.Models;
using FBTargeting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepTargeting.Services
{
    public interface IQueryService
    {
        public List<Interest> GetKeywordInterests(string queryText);
    }
}
