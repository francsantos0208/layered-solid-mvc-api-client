using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class ActorDto
    {
        public string Name { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}
