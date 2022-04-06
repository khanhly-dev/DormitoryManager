using Dormitory.Admin.Application.CommonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.RoomRepository.Dtos
{
    public class RoomSelectDto : ComboSelectDto
    {
        public int? GenderRoom { get; set; }
        public int? AvaiableSlot { get; set; }
    }
}
