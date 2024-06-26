﻿using Microsoft.EntityFrameworkCore;
using MKTDotNetCore.DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MKTDotNetCore.DataAcess.Db
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        public DbSet<BlogModel> blogs { get; set; }
    }
}
