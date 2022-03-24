using Dormitory.Core.Application.Catalog.CoreRepository.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Core.Application.Catalog.CoreRepository
{
    public interface ICoreRepo
    {
        public Task<LoginStatusDto> Authenticate(string userName, string password, int tenantId);
        public Task<int> Register(RegisterRequest request);
    }
}
