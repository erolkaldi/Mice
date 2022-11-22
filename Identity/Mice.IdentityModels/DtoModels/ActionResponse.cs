using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.IdentityModels.DtoModels
{
    public class ActionResponse<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string Exception { get; set; } = "";
        public T Data { get; set; }
    }
}
