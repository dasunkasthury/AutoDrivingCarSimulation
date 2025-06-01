using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Domain.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
