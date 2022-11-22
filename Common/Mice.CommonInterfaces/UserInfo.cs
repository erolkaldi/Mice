using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.CommonInterfaces
{
    public class UserInfo
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
