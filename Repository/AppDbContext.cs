using DotIndiaPvtLtd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotIndiaPvtLtd.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Users> users { get; set; }
        public DbSet<Forms> Forms { get; set; }
        public DbSet<FormQuery> FormQuery { get; set; }
    }
}
