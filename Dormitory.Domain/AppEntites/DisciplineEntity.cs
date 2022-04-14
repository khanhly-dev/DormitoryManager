using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Domain.AppEntites
{
    public class DisciplineEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Description { get; set; }
        public string Punish { get; set; }
    }
}
