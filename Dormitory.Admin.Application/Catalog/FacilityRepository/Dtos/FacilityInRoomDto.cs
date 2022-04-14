using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Admin.Application.Catalog.FacilityRepository.Dtos
{
    public class FacilityInRoomDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int FacilityId { get; set; }
        public string FacilityName { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
    }
}
