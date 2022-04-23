using Dormitory.Admin.Application.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ContractTimeConfigRepository.Dtos
{
    public class ContractConfigSelectDto : ComboSelectDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
