using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Domain.Entity;
using AutoMapper;

namespace AutoDrivingCarSimulator.Core.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarDto, EntityCar>()
                .ForMember(dest => dest.Command, opt => opt.MapFrom(src => string.Join("", src.CommandList)));
            CreateMap<EntityCar, CarDto>()
                .ForMember(dest => dest.CommandList, opt => opt.MapFrom(src => src.Command.Select(c => Enum.Parse<Command>(c.ToString())).ToList()));
            CreateMap<EntityField, FieldDto>().ReverseMap();
        }
    }
}
