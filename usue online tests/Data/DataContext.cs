using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using usue_online_tests.Models;

namespace usue_online_tests.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TestPreset> Presets { get; set; }

        public DataContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "   Host=localhost;Port=5432;Database=usue_online_tests;Username=postgres;Password=padmin");
        }
    }
}
