using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.CommonInterfaces
{
    public interface IUserRepository
    {
        UserInfo User { get; }
    }
}
