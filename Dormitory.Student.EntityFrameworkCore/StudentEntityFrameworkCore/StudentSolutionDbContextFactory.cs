using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.EntityFrameworkCore.StudentEntityFrameworkCore
{
    public class StudentSolutionDbContextFactory : IDesignTimeDbContextFactory<StudentSolutionDbContext>
    {
        public StudentSolutionDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connetionString = configuration.GetConnectionString("StudentDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<StudentSolutionDbContext>();
            optionsBuilder.UseSqlServer(connetionString);
            return new StudentSolutionDbContext(optionsBuilder.Options);
        }
    }
}
