using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class CamelRegistryDbContext: DbContext
    {
        public CamelRegistryDbContext(DbContextOptions<CamelRegistryDbContext> options) : base(options) {}
        public DbSet<Camel> Camels { get; set; }
    }
}
