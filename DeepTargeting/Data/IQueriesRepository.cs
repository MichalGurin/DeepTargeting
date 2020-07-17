using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using DeepTargeting.Models;

namespace DeepTargeting.Data
{
    public interface IQueriesRepository
    {
        public Task AddQueryToDBAsync(Query query);

        public List<Query> GetQueriesOfUser(ClaimsPrincipal user);

        public bool QueryNotInDB(Query query);
    }
}
