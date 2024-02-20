using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using OfficeOpenXml.FormulaParsing.Exceptions;
using usue_online_tests.Models;
using Console = System.Console;

namespace usue_online_tests.Data
{
    public class DataContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public DbSet<User> Users { get; set; }
        public DbSet<TestPreset> Presets { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<UserExamResult> UserExamResults { get; set; }
        public DbSet<ExamTestAnswer> ExamTestAnswers { get; set; }
        public DbSet<PredictionResult> PredictionResults { get; set; }
        public DbSet<PredictionCategory> PredictionCategories { get; set; }

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // KeyValuePair<string, string> envVarConnectionString = Configuration.AsEnumerable().FirstOrDefault(pair => pair.Key == "pgConnectionString");

            // if (string.IsNullOrWhiteSpace(envVarConnectionString.Value))
            //     throw new Exception("pgConnectionString is null");

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usue_online_tests;Username=postgres;Password=1");
        }
    }
}
