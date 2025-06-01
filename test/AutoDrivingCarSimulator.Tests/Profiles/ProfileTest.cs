using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Core.Profiles;
using AutoMapper;
using FluentAssertions;

namespace AutoDrivingCarSimulator.Tests.Profiles
{
    public class ProfileTest
    {
        [Fact]
        public void GivenValidCarDto_Expect_MappedEntity()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarProfile>());
            var mapper = config.CreateMapper();
            var src = new CarDto { Name = "A", XCoordinate = 5, YCoordinate = 7, Direction = Direction.N };

            //Act
            var dest = mapper.Map<CarDto>(src);

            //Assertion
            dest.Name.Should().Be(src.Name);
            dest.XCoordinate.Should().Be(src.XCoordinate);
            dest.YCoordinate.Should().Be(src.YCoordinate);
            dest.Direction.Should().Be(src.Direction);
        }

        [Fact]
        public void GivenValidFieldDto_Expect_MappedEntity()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<FieldProfile>());
            var mapper = config.CreateMapper();
            var src = new FieldDto { Height = 10, Width = 10 };

            //Act
            var dest = mapper.Map<FieldDto>(src);

            //Assertion
            dest.Height.Should().Be(src.Height);
            dest.Width.Should().Be(src.Width);
        }
    }
}
