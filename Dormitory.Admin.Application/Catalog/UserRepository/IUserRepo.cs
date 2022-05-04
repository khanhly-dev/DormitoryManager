using Dormitory.Admin.Application.Catalog.UserRepository.Dtos;
using Dormitory.Admin.Application.CommonDto;
using Dormitory.Domain.AppEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.UserRepository
{
    public interface IUserRepo
    {
        Task<List<UserDto>> GetAllUser();
        Task<PageResult<UserInfoEntity>> GetListUser(PageRequestBase request);
        Task<int> CreateOrUpdateUser(UserInfoEntity request);
        Task<int> DeleteUser(int id);
        Task<int> DeleteAccount(int id);
        Task<UserDto> GetUserAccountByInfo(int userInfoId, int tenant);
    }
}
