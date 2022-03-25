using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.FacilityRepository.Dtos
{
    public class FacilityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalCount { get; set; }
        public string Status { get; set; }
    }
}
