using freeCourse.Shared.Dtos;
using FreeCourse.Services.Order.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Queries
{
    public class GetOdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
