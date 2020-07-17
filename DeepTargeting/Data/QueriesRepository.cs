using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using DeepTargeting.Models;

namespace DeepTargeting.Data
{
    public class QueriesRepository : IQueriesRepository
    {
        private readonly ApplicationDbContext dbContext;

        public QueriesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddQueryToDBAsync(Query query)
        {
            await dbContext.AllQueries.AddAsync(query);
            await dbContext.SaveChangesAsync();
        }

        public List<Query> GetQueriesOfUser(ClaimsPrincipal user)
        {
            List<Query> pastQueries = dbContext.AllQueries.Where(x => x.UserId == user.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
            return pastQueries;
        }

        public bool QueryNotInDB(Query query)
        {
            if (query.QueryText == "")
            {
                return false;
            }

            return dbContext.AllQueries.Where(q => q.QueryText == query.QueryText &&
            q.UserId == query.UserId).ToList().Count == 0;
        }
    }
}
