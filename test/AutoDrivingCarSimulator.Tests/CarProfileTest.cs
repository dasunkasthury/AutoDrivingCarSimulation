using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Core.Profiles;
using AutoDrivingCarSimulator.Domain.Entity;
using AutoDrivingCarSimulator.Infrastructure.Repositories;
using AutoDrivingCarSimulator.Tests.Helpers;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace AutoDrivingCarSimulator.Tests
{
    public class CarProfileTest
    {
        [Fact]
        public void GivenValidDto_Expect_MappedEntity()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarProfile>());
            var mapper = config.CreateMapper();
            var src = new CarDto { Name = "A", XCoordinate = 5, YCoordinate = 7, Direction = Direction.N };


            //Act
            var dest = mapper.Map<CarDto>(src);


            //Assertion
            Assert.Equal(dest.Name, src.Name);
            Assert.Equal(dest.XCoordinate, src.XCoordinate);
            Assert.Equal(dest.YCoordinate, src.YCoordinate);
            Assert.Equal(dest.Direction, src.Direction);
        }
    }
}
