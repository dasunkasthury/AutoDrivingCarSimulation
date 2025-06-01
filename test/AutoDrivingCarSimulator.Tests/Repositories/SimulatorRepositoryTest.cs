using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Core.Profiles;
using AutoDrivingCarSimulator.Infrastructure.Repositories;
using AutoDrivingCarSimulator.Tests.Helpers;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;

namespace AutoDrivingCarSimulator.Tests.Repositories
{
    public class SimulatorRepositoryTest
    {
        [Theory, InlineAutoData("A", 0, 0, Direction.N, "R")]
        public void GivenCars_Get_completedCarList(string name, int xCord, int yCord, Direction Direction, string command)
        {
            // Arrange
            var car1 = SimulatorServiceHelper.GetCar(name, xCord, yCord, Direction, command, false);
            var car2 = SimulatorServiceHelper.GetCar(name, xCord, yCord, Direction, command, true);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarProfile>());
            var mapper = config.CreateMapper();
            var slut = new SimulatorRepository(mapper);
            slut.AddCar(car1);
            slut.AddCar(car2);

            //Act
            var res = slut.GetCompletedCars();

            //Assertion
            res.Should().HaveCount(1);
            res.First().IsCollide.Should().BeFalse();
        }

        [Theory, InlineAutoData("A", 0, 0, Direction.N, "R")]
        public void GivenCars_Get_collidedCarList(string name, int xCord, int yCord, Direction Direction, string command)
        {
            // Arrange
            var car1 = SimulatorServiceHelper.GetCar(name, xCord, yCord, Direction, command, false);
            var car2 = SimulatorServiceHelper.GetCar(name, xCord, yCord, Direction, command, true);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarProfile>());
            var mapper = config.CreateMapper();
            var slut = new SimulatorRepository(mapper);
            slut.AddCar(car1);
            slut.AddCar(car2);

            //Act
            var res = slut.GetCollidedCars();

            //Assertion
            res.Should().HaveCount(1);
            res.First().IsCollide.Should().BeTrue();
        }


    }
}
