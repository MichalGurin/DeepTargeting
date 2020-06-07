using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DeepTargeting.Models;

namespace DeepTargeting.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Query> QueryTexts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
