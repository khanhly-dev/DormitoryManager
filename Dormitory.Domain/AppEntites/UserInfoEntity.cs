using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Domain.AppEntities
{
    public class UserInfoEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public string Position { get; set; }
        public string Adress { get; set; }
    }
}
