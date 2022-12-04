using AutoMapper;

namespace WebApplication1.Application.User.Create
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RequestCreate, DAL.Domain.User>();
            CreateMap<DAL.Domain.User, ResponseCreate>();
        }
    }
}
