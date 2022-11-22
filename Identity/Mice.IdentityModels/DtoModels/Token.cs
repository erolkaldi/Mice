using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.IdentityModels.DtoModels
{
    public class Token
    {
        public string Access_Token { get; set; } = "";
        public DateTime Expiration { get; set; }
    }
}
