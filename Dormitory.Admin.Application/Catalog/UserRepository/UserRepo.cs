using Dormitory.Admin.Application.Catalog.UserRepository.Dtos;
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
    }
}
