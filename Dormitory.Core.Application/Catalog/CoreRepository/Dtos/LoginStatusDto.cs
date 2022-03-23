using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Core.Application.Catalog.CoreRepository.Dtos
{
    public class LoginStatusDto
    {
        public string UserName { get; set; }
        public string Access_token { get; set; }
        public bool IsLoginSuccess { get; set; }
    }
}
