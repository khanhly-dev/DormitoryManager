using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore
{
    public class AdminSolutionDbContextFactory : IDesignTimeDbContextFactory<AdminSolutionDbContext>
    {
        public AdminSolutionDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connetionString = configuration.GetConnectionString("AdminDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<AdminSolutionDbContext>();
            optionsBuilder.UseSqlServer(connetionString);
            return new AdminSolutionDbContext(optionsBuilder.Options);
        }
    }
}
