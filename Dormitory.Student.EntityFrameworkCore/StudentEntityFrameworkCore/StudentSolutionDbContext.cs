using Dormitory.Domain.StudentEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.EntityFrameworkCore.StudentEntityFrameworkCore
{
    public class StudentSolutionDbContext : DbContext
    {
        public StudentSolutionDbContext(DbContextOptions<StudentSolutionDbContext> option) : base(option)
        {

        }

        public DbSet<StudentEntity> StudentEntities { get; set; }
        public DbSet<UserAccountEntity> UserAccountEntities { get; set; }
    }
}
