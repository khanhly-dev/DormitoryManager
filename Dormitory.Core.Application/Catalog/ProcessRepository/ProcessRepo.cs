using Dormitory.Domain.Shared.Constant;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Core.Application.Catalog.ProcessRepository
{
    public class ProcessRepo : IProcessRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public ProcessRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> ProcessUpdateContractType()
        {
            var listContract = await _dbContext.ContractEntities.ToListAsync();
            foreach (var item in listContract)
            {
                if(item.FromDate <= DateTime.Now && item.ToDate > DateTime.Now)
                {
                    item.IsExtendContract = false;
                }
            }
           return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> ProcessUpdateRoom()
        {
            var listStudent = await _dbContext.StudentEntities.ToListAsync();
            foreach (var item in listStudent)
            {
                //lay ra hop dong cuoi cung cua sv
                var contract = await _dbContext.ContractEntities
                    .Where(x => x.ContractCompletedStatus == DataConfigConstant.contractCompletedStatusOk)
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync();

                if(contract.ToDate < DateTime.Now)
                {
                    if (contract.RoomId.HasValue)
                    {
                        var room = await _dbContext.RoomEntities.FirstOrDefaultAsync(x => x.Id == contract.RoomId);
                        if ((contract.IsDeleted == false && contract.ToDate < DateTime.Now) || contract.IsDeleted == true)
                        {
                            room.FilledSlot -= 1;
                            room.EmptySlot += 1;
                            room.AvaiableSlot += 1;
                            if (room.FilledSlot == 0)
                            {
                                room.RoomAcedemic = null;
                                room.RoomGender = null;
                            }
                        }
                    }
                }    
            }
            return await _dbContext.SaveChangesAsync();
        }
    }
}
