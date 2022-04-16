using Dormitory.Domain.AppEntites;
using Dormitory.Domain.AppEntities;
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
        public DbSet<UserInfoEntity> UserInfoEntities { get; set; }
        public DbSet<AreaEntity> AreaEntities { get; set; }
        public DbSet<ContractEntity> ContractEntities { get; set; }
        public DbSet<CriteriaConfigEntity> CriteriaConfigEntities { get; set; }
        public DbSet<FacilityEntity> FacilityEntities { get; set; }
        public DbSet<FacilityInRoomEntity> FacilityInRoomEntities { get; set; }
        public DbSet<RoomEntity> RoomEntities { get; set; }
        public DbSet<RoomServiceEntity> RoomServiceEntities { get; set; }
        public DbSet<ServiceEntity> ServiceEntities { get; set; }
        public DbSet<StudentCriteriaEntity> StudentCriteriaEntities { get; set; }
        public DbSet<StudentEntity> StudentEntities { get; set; }
        public DbSet<ContractFeeEntity> ContractFeeEntities { get; set; }
        public DbSet<ContractTimeConfigEntity> ContractTimeConfigEntities { get; set; }
        public DbSet<ServiceContractEntity> ServiceContractEntities { get; set; }
        public DbSet<NotificationEntity> NotificationEntities { get; set; }
        public DbSet<RoomServiceFeeEntity> RoomServiceFeeEntities { get; set; }
        public DbSet<BillServiceEntity> BillServiceEntities { get; set; }
        public DbSet<DisciplineEntity> DisciplineEntities { get; set; }

    }
}
