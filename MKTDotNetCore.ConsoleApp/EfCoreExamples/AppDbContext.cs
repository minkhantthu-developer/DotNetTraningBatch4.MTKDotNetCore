using Microsoft.EntityFrameworkCore;
using MKTDotNetCore.ConsoleApp.Dtos;
using MKTDotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTDotNetCore.ConsoleApp.EfCoreExamples
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogDto> blogs { get; set; }
    }
}
