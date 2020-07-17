using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using DeepTargeting.Models;
using System.Collections.Generic;
using System.Security.Claims;

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
