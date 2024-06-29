using Microsoft.EntityFrameworkCore;
using MKTDotNetCore.MinimalApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTDotNetCore.MinimalApi.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BlogModel> blogs { get; set; }
    }
}
