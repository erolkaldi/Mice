using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.IdentityModels.EntityModels
{
    public class CompanyUser : BaseEntity
    {
        [MaxLength(100)]
        public string FirstName { get; set; } = "";
        [MaxLength(100)]
        public string LastName { get; set; } = "";
        [MaxLength(100)]
        public string Email { get; set; } = "";
        [MaxLength(50)]
        public string Phone { get; set; } = "";
        public bool EmailVerified { get; set; }
        public Guid CompanyId { get; set; }
        [MaxLength(100)]
        public string Password { get; set; } = "";
        public short RetryCount { get; set; }
        public bool Blocked { get; set; }
    }
}
