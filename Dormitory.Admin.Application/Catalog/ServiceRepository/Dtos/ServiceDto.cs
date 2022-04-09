using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.ServiceRepository.Dtos
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int ServiceType { get; set; }
        public string Unit { get; set; }

    }
}
