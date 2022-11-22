using MediatR;
using Mice.CommonInterfaces;
using Mice.PublisherContext;
using Mice.PublisherModels.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mice.PublisherServices.Features.Events.Queries
{
    public class GetEventQuery : IRequest<ActionResponse<EventDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetEventQueryHandler : IRequestHandler<GetEventQuery,ActionResponse<EventDto>> {
        private PublisherDbContext _db;
        private readonly IUserRepository _userRepository;
        public GetEventQueryHandler(PublisherDbContext db,IUserRepository userRepository)
        {
            _db = db;
            _userRepository = userRepository;
        }

        public async Task<ActionResponse<EventDto>> Handle(GetEventQuery query,CancellationToken cancellationToken)
        {
            ActionResponse<EventDto> response = new ActionResponse<EventDto>();
            try
            {

            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
}
