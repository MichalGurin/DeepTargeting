using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using DeepTargeting.Models;


namespace DeepTargeting.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Query> AllQueries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public async Task AddQueryToDB(Query query)
        {
            await AllQueries.AddAsync(query);
            await SaveChangesAsync();
        }

        public bool QueryNotInDB(Query query)
        {
            if (query.QueryText == "")
            {
                return false;
            }

            return AllQueries.Where(q => q.QueryText == query.QueryText &&
            q.UserId == query.UserId).ToList().Count == 0;
        }
    }
}
