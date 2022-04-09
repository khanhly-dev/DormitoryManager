using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Domain.AppEntities
{
    public class ServiceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Unit { get; set; }
        public int ServiceType { get; set; }
    }
}
