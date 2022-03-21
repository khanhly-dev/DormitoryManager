using Dormitory.Admin.Application.Catalog.UserRepository.Dtos;
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
    }
}
