using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.PublisherModels.DtoModels
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public Guid CompanyId { get; set; }

    }
}
