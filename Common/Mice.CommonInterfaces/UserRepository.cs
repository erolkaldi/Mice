using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.CommonInterfaces
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserInfo User
        {
            get
            {
                var user = new UserInfo()
                {
                    FirstName = _httpContextAccessor.HttpContext.User.FindFirst("FirstName")?.Value ?? "",
                    LastName = _httpContextAccessor.HttpContext.User.FindFirst("LastName")?.Value ?? "",
                    Email = _httpContextAccessor.HttpContext.User.FindFirst("Email")?.Value ?? "",
                    UserId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("UserId")?.Value ?? ""),
                    CompanyId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst("CompanyId")?.Value ?? Guid.Empty.ToString())
                };
                return user;
            }
        }

    }
}
