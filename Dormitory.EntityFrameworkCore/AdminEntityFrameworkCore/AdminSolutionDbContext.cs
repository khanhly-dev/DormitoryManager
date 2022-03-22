using Dormitory.Domain.AdminEntites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore
{
    public class AdminSolutionDbContext : DbContext
    {
        public AdminSolutionDbContext(DbContextOptions<AdminSolutionDbContext> options) : base(options)
        {

        }

        public DbSet<UserAccountEntity> UserAccountEntities { get; set; }
        public DbSet<UserAdminEntity> UserAdminEntities { get; set; }
    }
}
