using Dormitory.Admin.Application.Catalog.ServiceRepository.Dtos;
using Dormitory.Admin.Application.Catalog.ServiceRepository.Requests;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntites;
using Dormitory.Domain.AppEntities;
using Dormitory.Domain.Shared.Constant;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ServiceRepository
{
    public class ServiceRepo : IServiceRepo
    {
        private readonly AdminSolutionDbContext _dbContext;
        public ServiceRepo(AdminSolutionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddServiceForRoom(List<AddServiceForRoomRequest> request, int roomId, DateTime fromDate, DateTime toDate)
        {
            if(request == null || request.Count == 0)
            {
                return 0;
            }
            var listCode = await _dbContext.BillServiceEntities.Select(x => x.Code).ToListAsync();
            var random = new Random();
            var contractCode = "DV" + random.Next(100000, 999999).ToString();
            while (listCode.Contains(contractCode))
            {
                contractCode = "DV" + random.Next(100000, 999999).ToString();
            }
            var billService = new BillServiceEntity
            {
                Code = contractCode,
                RoomId = roomId,
                FromDate = fromDate,
                ToDate = toDate,
            };
            _dbContext.BillServiceEntities.Add(billService);
            await _dbContext.SaveChangesAsync();

            foreach (var item in request)
            {
                var roomService = new RoomServiceEntity
                {
                    Id = 0,
                    RoomId = roomId,
                    ServiceId = item.ServiceId,
                    StatBegin = item.StatBegin,
                    StatEnd = item.StatEnd,
                    Quantity = item.StatEnd - item.StatBegin,
                    BillId = billService.Id
                };
                _dbContext.RoomServiceEntities.Add(roomService);

                await _dbContext.SaveChangesAsync();

                var service = await _dbContext.ServiceEntities.FirstOrDefaultAsync(x => x.Id == item.ServiceId);
                var serviceRoomPrice = service.Price * roomService.Quantity;

                var serviceRoomFee = new RoomServiceFeeEntity
                {
                    RoomServiceId = roomService.Id,
                    ServicePrice = serviceRoomPrice,
                    MoneyPaid = 0,
                    IsPaid = false
                };
                _dbContext.RoomServiceFeeEntities.Add(serviceRoomFee);
            }
           
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CreateOrUpdate(ServiceEntity request)
        {
            var service = new ServiceEntity()
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                ServiceType = request.ServiceType,
                Unit = request.Unit,
            };
            if (service.Id == 0)
            {
                _dbContext.ServiceEntities.Add(service);
                return await _dbContext.SaveChangesAsync();
            }
            else if (service.Id > 0)
            {
                _dbContext.ServiceEntities.Update(service);
                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Delete(int id)
        {
            var service = await _dbContext.ServiceEntities.FindAsync(id);
            if (service != null)
            {
                _dbContext.ServiceEntities.Remove(service);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteBillService(int billId)
        {
            var billService = await _dbContext.BillServiceEntities.FindAsync(billId);
            if(billService != null)
            {
                var listRoomService = await _dbContext.RoomServiceEntities.Where(x => x.BillId == billId).ToListAsync();
                var listRoomServiceFee = await _dbContext.RoomServiceFeeEntities.Where(x => listRoomService.Select(x => x.Id).ToList().Contains(x.RoomServiceId)).ToListAsync();
                _dbContext.RoomServiceFeeEntities.RemoveRange(listRoomServiceFee);
                _dbContext.RoomServiceEntities.RemoveRange(listRoomService);
                _dbContext.BillServiceEntities.Remove(billService);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRoomServiceFee(int roomServiceId)
        {
            var roomService = await _dbContext.RoomServiceEntities.FirstOrDefaultAsync(x => x.Id == roomServiceId);
            if(roomService != null)
            {
                _dbContext.RoomServiceEntities.Remove(roomService);
            }
            var roomServiceFee = await _dbContext.RoomServiceFeeEntities.FirstOrDefaultAsync(x => x.RoomServiceId == roomServiceId);
            if (roomServiceFee != null)
            {
                _dbContext.RoomServiceFeeEntities.Remove(roomServiceFee);
            }
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<PageResult<ServiceDto>> GetList(PageRequestBase request)
        {
            var query = from a in _dbContext.ServiceEntities
                        select a;

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ServiceDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Unit = x.Unit,
                    ServiceType = x.ServiceType
                }).ToListAsync();

            var pageResult = new PageResult<ServiceDto>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<List<ComboSelectDto>> GetListSelect()
        {
            var serviceList = await _dbContext.ServiceEntities.Where(x => x.ServiceType == DataConfigConstant.RoomService).Select(x => new ComboSelectDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return serviceList;
        }

        public async Task<List<RoomServiceDto>> GetServiceByBill(int billId)
        {
            var listService = from rs in _dbContext.RoomServiceEntities
                              join s in _dbContext.ServiceEntities on rs.ServiceId equals s.Id
                              join rsf in _dbContext.RoomServiceFeeEntities on rs.Id equals rsf.RoomServiceId
                              where rs.BillId == billId
                              select new { rs, s, rsf };
            var data = await listService.Select(x => new RoomServiceDto
            {
                Id = x.rs.Id,
                RoomId = x.rs.RoomId,
                ServiceId = x.rs.ServiceId,
                ServiceName = x.s.Name,
                Quantity = x.rs.Quantity,
                StatBegin = x.rs.StatBegin,
                StatEnd = x.rs.StatEnd,
                TotalServicePrice = x.rsf.ServicePrice,
                PaidDate = x.rsf.PaidDate,
                MoneyPaid = x.rsf.MoneyPaid,
                IsPaid = x.rsf.IsPaid,
            }).ToListAsync();

            return data;
        }

        public async Task<List<RoomServiceDto>> GetServiceByRoom(int roomId)
        {
            var listService = from rs in _dbContext.RoomServiceEntities
                              join s in _dbContext.ServiceEntities on rs.ServiceId equals s.Id
                              join rsf in _dbContext.RoomServiceFeeEntities on rs.Id equals rsf.RoomServiceId
                              where rs.RoomId == roomId
                              select new { rs, s, rsf };
            var data = await listService.Select(x => new RoomServiceDto
            {
                Id = x.rs.Id,
                RoomId = x.rs.RoomId,
                ServiceId = x.rs.ServiceId,
                ServiceName = x.s.Name,
                Quantity = x.rs.Quantity,
                StatBegin = x.rs.StatBegin,
                StatEnd = x.rs.StatEnd,
                TotalServicePrice = x.rsf.ServicePrice,
                PaidDate = x.rsf.PaidDate,
                MoneyPaid = x.rsf.MoneyPaid,
                IsPaid = x.rsf.IsPaid,
            }).ToListAsync();

            return data;
        }

        public async Task<int> UpdateRoomServiceFee(int roomServiceId, float moneyPaid, DateTime datePaid)
        {
            var roomServiceFee = await _dbContext.RoomServiceFeeEntities.FirstOrDefaultAsync(x => x.RoomServiceId == roomServiceId);
            if(roomServiceFee != null)
            {
                roomServiceFee.MoneyPaid = moneyPaid;
                roomServiceFee.PaidDate = datePaid;
                if(moneyPaid >= roomServiceFee.ServicePrice )
                {
                    roomServiceFee.IsPaid = true;
                }
                else
                {
                    roomServiceFee.IsPaid = false;
                }
            }

            return await _dbContext.SaveChangesAsync();
        }
    }
}
