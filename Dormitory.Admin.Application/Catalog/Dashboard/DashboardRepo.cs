using Dormitory.Admin.Application.Catalog.Dashboard.Dtos;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.Dashboard
{
    public class DashboardRepo : IDashboardRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public DashboardRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BaseStatDto> GetBaseStat()
        {
            var totalSignUp = (await _dbContext.ContractEntities.Where(x => x.AdminConfirmStatus == null).ToListAsync()).Count;
            var totalEmptySlot = (await _dbContext.RoomEntities.ToListAsync()).Sum(x => x.EmptySlot) ?? 0;


            //dem so sinh vien no phi
            var countContractIsDept = 0;
            var query = await (from s in _dbContext.StudentEntities
                               select s).ToListAsync();
            foreach (var item in query)
            {
                var listContract = await _dbContext.ContractEntities.Where(x => x.StudentId == item.Id).ToListAsync();
                var listFee = await _dbContext.ContractFeeEntities.Where(t => listContract.Select(x => x.Id).Contains(t.ContractId)).ToListAsync();

                if ((listFee.Sum(x => x.ContractPriceValue) - listFee.Sum(x => x.MoneyPaid.Value)) > 0)
                {
                    countContractIsDept++;
                }   
            }
            //dem so phong no phi
            var countRoomIsDept = 0;
            var query1 = await (from r in _dbContext.RoomEntities
                                select r).ToListAsync();

            foreach (var item in query1)
            {
                var listRoomService = await _dbContext.RoomServiceEntities.Where(x => x.RoomId == item.Id).ToListAsync();
                var listRoomServiceFee = await _dbContext.RoomServiceFeeEntities.Where(t => listRoomService.Select(x => x.Id).ToList().Contains(t.RoomServiceId)).ToListAsync();
        
                if((listRoomServiceFee.Sum(x => x.ServicePrice) - listRoomServiceFee.Sum(x => x.MoneyPaid.Value))>0)
                {
                    countRoomIsDept++;
                }    
            }

            var data = new BaseStatDto
            {
                TotalContractDept = countContractIsDept,
                TotalEmptySlot = totalEmptySlot,
                TotalSignUp = totalSignUp,
                TotalServiceDept = countRoomIsDept,
            };
            return data;
        }
    }
}
