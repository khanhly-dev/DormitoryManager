using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Student.Application.CommonDto
{
    public class PageResult<T> : PageResultBase
    {
        public List<T> Items { get; set; }
    }
}
