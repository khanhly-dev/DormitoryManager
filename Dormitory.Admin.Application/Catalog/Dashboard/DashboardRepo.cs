using Dormitory.Admin.Application.Catalog.Dashboard.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.Shared.Constant;
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

        public async Task<List<AreaChart>> GetAreaChart()
        {
            var listData = new List<AreaChart>();
            var listArea = await _dbContext.AreaEntities.ToListAsync();

            foreach (var item in listArea)
            {
                var listRoom = await _dbContext.RoomEntities.Where(x => x.AreaId == item.Id).ToListAsync(); ;
                var totalFilledSlot = listRoom.Sum(x => x.FilledSlot);
                var data = new AreaChart
                {
                    Area = item.Name,
                    StudentCount = totalFilledSlot ?? 0
                };
                listData.Add(data);
            }

            return listData;
        }

        public async Task<BaseStatDto> GetBaseStat()
        {
            var totalSignUp = (await _dbContext.ContractEntities.Where(x => x.AdminConfirmStatus == null || x.AdminConfirmStatus == DataConfigConstant.contractConfirmStatusPending).ToListAsync()).Count;
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

        public async Task<PageResult<FeeDto>> GetContractFee(PageRequestBase request)
        {
            var query = from c in _dbContext.ContractEntities
                        join cf in _dbContext.ContractFeeEntities on c.Id equals cf.ContractId
                        select new { c, cf };

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new FeeDto
                {
                    Id = x.c.Id,
                    BillCode = x.c.ContractCode,
                    Fee = x.cf.ContractPriceValue
                }).ToListAsync();

            var pageResult = new PageResult<FeeDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<FeeChart> GetContractFeeChart()
        {
            var feeChart = await _dbContext.ContractFeeEntities.ToListAsync();
            var data = new FeeChart
            {
                TotalFee = feeChart.Sum(x => x.ContractPriceValue),
                Dept = feeChart.Sum(x => x.ContractPriceValue) - feeChart.Sum(x => x.MoneyPaid) ?? 0,
                Paid = feeChart.Sum(x => x.MoneyPaid) ?? 0,
            };

            return data;
        }

        public async Task<GenderPercentDto> getGenderPercent()
        {
            var listContract = await _dbContext.ContractEntities
                .Where(x => x.ContractCompletedStatus == DataConfigConstant.contractCompletedStatusOk && x.ToDate.Value > DateTime.Now && x.IsDeleted == false)
                .ToListAsync();

            var listStudent = await _dbContext.StudentEntities.Where(x => listContract.Select(x => x.StudentId).ToList().Contains(x.Id)).ToListAsync();

            var countMale = listStudent.Where(x => x.Gender == DataConfigConstant.male).ToList().Count;
            var countFemale = listStudent.Where(x => x.Gender == DataConfigConstant.female).ToList().Count;

            var data = new GenderPercentDto
            {
                CountMale = countMale,
                CountFemale = countFemale
            };

            return data;
        }

        public async Task<PageResult<FeeDto>> GetServiceFee(PageRequestBase request)
        {
            var listBill = await _dbContext.BillServiceEntities.Select(x => new FeeDto
            {
                Id = x.Id,
                BillCode = x.Code,
                Fee = 0
            }).ToListAsync();
            foreach (var item in listBill)
            {
                var roomServiceList = await _dbContext.RoomServiceEntities.Where(x => x.BillId == item.Id).ToListAsync();
                var roomServiceFeeList = await _dbContext.RoomServiceFeeEntities.Where(x => roomServiceList.Select(x => x.Id).ToList().Contains(x.RoomServiceId)).ToListAsync();
                item.Fee = roomServiceFeeList.Sum(x => x.ServicePrice);
            }

            int totalRow = listBill.Count;

            var data = listBill
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new FeeDto
                {
                    Id = x.Id,
                    BillCode = x.BillCode,
                    Fee = x.Fee
                }).ToList();

            var pageResult = new PageResult<FeeDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<FeeChart> GetServiceFeeChart()
        {
            var feeChart = await _dbContext.RoomServiceFeeEntities.ToListAsync();
            var data = new FeeChart
            {
                TotalFee = feeChart.Sum(x => x.ServicePrice),
                Dept = feeChart.Sum(x => x.ServicePrice) - feeChart.Sum(x => x.MoneyPaid) ?? 0,
                Paid = feeChart.Sum(x => x.MoneyPaid) ?? 0,
            };

            return data;
        }
    }
}
