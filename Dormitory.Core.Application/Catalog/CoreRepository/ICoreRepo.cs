using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Core.Application.Catalog.CoreRepository
{
    public interface ICoreRepo
    {
        public Task<string> Authenticate(string userName, string password, int tenantId);
    }
}
