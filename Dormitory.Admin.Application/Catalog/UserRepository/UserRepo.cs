using Dormitory.Admin.Application.Catalog.UserRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using Dormitory.EntityFrameworkCore.AdminEntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.UserRepository
{
    public class UserRepo : IUserRepo
    {
        private readonly AdminSolutionDbContext _adminDbContext;
        public UserRepo(AdminSolutionDbContext adminDbContext)
        {
            _adminDbContext = adminDbContext;
        }

        public async Task<int> CreateOrUpdateUser(UserInfoEntity request)
        {
            if(request.Id != 0)
            {
                _adminDbContext.UserInfoEntities.Update(request);
            }
            else
            {
                _adminDbContext.UserInfoEntities.Add(request);
            }
            return await _adminDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAccount(int id)
        {
            var userAccount = await _adminDbContext.UserAccountEntities.FindAsync(id);
            if (userAccount != null)
            {
                _adminDbContext.UserAccountEntities.Remove(userAccount);
            }
            return await _adminDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteUser(int id)
        {
            var userInfo = await _adminDbContext.UserInfoEntities.FindAsync(id);
            if (userInfo != null)
            {
                _adminDbContext.Remove(userInfo);
            }
            var userAccount = await _adminDbContext.UserAccountEntities.FirstOrDefaultAsync(x => x.UserInfoId == id && x.Tenant == 1);
            if(userAccount != null)
            {
                _adminDbContext.UserAccountEntities.Remove(userAccount);
            }
            return await _adminDbContext.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllUser()
        {
            var listUser = await _adminDbContext.UserAccountEntities.AsNoTracking().Select(x => new UserDto
            {
                Id = x.Id,
                UserName = x.UserName,  
                Password = x.Password,
            }).ToListAsync();
            return listUser;
        }

        public async Task<PageResult<UserInfoEntity>> GetListUser(PageRequestBase request)
        {
            var query = from s in _adminDbContext.UserInfoEntities
                        select s;

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var pageResult = new PageResult<UserInfoEntity>()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<UserDto> GetUserAccountByInfo(int userInfoId, int tenant)
        {
            var userAccount = await _adminDbContext.UserAccountEntities.Where(x => x.UserInfoId == userInfoId && x.Tenant == tenant).Select(z => new UserDto
            {
                Id = z.Id,
                UserName = z.UserName,
                Password = ""
            }).FirstOrDefaultAsync();
            return userAccount;
        }
    }
}
