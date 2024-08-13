using AutoMapper;
using Shared.Dtos.Order;
using Shared.Dtos.User;

namespace Application.Common.Mappings
{
    public class ApplicationMapperProfile : Profile
    {
        public ApplicationMapperProfile()
        {
            CreateMap<Domain.Entities.Order, OrderDto>();
            CreateMap<Domain.Entities.User, UserDto>();
        }
    }
}
