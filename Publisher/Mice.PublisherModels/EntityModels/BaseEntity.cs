using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.PublisherModels.EntityModels
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        [MaxLength(100)]
        public string CreateUser { get; set; } = "";
        public bool Deleted { get; set; }
        public DateTime? DeleteDate { get; set; }
        [MaxLength(100)]
        public string DeleteUser { get; set; } = "";
    }
}
