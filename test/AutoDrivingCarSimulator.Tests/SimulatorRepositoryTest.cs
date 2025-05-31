using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Core.Profiles;
using AutoDrivingCarSimulator.Domain.Entity;
using AutoDrivingCarSimulator.Infrastructure.Repositories;
using AutoDrivingCarSimulator.Tests.Helpers;
using AutoFixture.Xunit2;
using AutoMapper;
using NSubstitute;

namespace AutoDrivingCarSimulator.Tests
{
    public class SimulatorRepositoryTest
    {
        [Theory, InlineAutoData("A", 5, 1, Direction.W, "F", 10, 10)]
        public void GivenCarCommandForword_Validate_carMoveForword(string name, int xCord, int yCord, Direction Direction, string command, int fieldWidth, int fieldHeight)
        {
            // Arrange
            var car = SimulatorServiceHelper.GetCar(name, xCord, yCord, Direction, command, false);
            var field = new FieldDto { Height = fieldHeight, Width = fieldWidth };

            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarProfile>());
            var mapper = config.CreateMapper();

            var slut = new SimulatorRepository(mapper);
            slut.AddCar(car);



            //Act
            slut.RunCommand(car, field);
            var res = slut.GetAllCar();

            //Assertion
            Assert.Single(res);
            Assert.Equal(xCord-1, res[0].XCoordinate);
            Assert.Equal(yCord, res[0].YCoordinate);
            Assert.Equal(car.Direction, res[0].Direction);


        }
    }
}
