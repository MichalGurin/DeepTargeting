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
    }
}
