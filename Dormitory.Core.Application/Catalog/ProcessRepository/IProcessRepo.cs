using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Core.Application.Catalog.ProcessRepository
{
    public interface IProcessRepo
    {
        Task<int> ProcessUpdateContractType();
        Task<int> ProcessUpdateRoom();
    }
}
