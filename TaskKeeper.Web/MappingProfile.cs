using AutoMapper;
using TaskKeeper.Domain;
using TaskKeeper.Web.Models;

namespace TaskKeeper.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateTaskItemRequest, TaskItem>();
        }
    }
}
