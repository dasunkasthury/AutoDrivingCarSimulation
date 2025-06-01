using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Domain.Entity;
using AutoMapper;

namespace AutoDrivingCarSimulator.Core.Profiles
{
    public class FieldProfile : Profile
    {
        public FieldProfile()
        {
            CreateMap<EntityField, FieldDto>().ReverseMap();
        }
    }
}
