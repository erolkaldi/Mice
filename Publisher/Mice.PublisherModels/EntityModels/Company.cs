using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.PublisherModels.EntityModels
{
    public class Company :BaseEntity
    {
        [MaxLength(250)]
        public string Name { get; set; } = "";
        [MaxLength(100)]
        public string Email { get; set; } = "";
        [MaxLength(50)]
        public string Phone { get; set; } = "";
        public bool EmailVerified { get; set; }
    }
}
