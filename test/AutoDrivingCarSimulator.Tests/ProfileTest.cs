using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Core.Profiles;
using AutoMapper;

namespace AutoDrivingCarSimulator.Tests
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
            Assert.Equal(dest.Name, src.Name);
            Assert.Equal(dest.XCoordinate, src.XCoordinate);
            Assert.Equal(dest.YCoordinate, src.YCoordinate);
            Assert.Equal(dest.Direction, src.Direction);
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
            Assert.Equal(dest.Height, src.Height);
            Assert.Equal(dest.Width, src.Width);
        }
    }
}
