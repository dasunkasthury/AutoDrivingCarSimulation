using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Domain.Entity;
using AutoDrivingCarSimulator.Infrastructure.Repositories;
using AutoDrivingCarSimulator.Tests.Helpers;
using AutoFixture.Xunit2;
using AutoMapper;
using NSubstitute;

namespace AutoDrivingCarSimulator.Tests
{
    public class SimulatorRepositoryTesting
    {
        [Theory, InlineAutoData("A", 0, 1, Direction.W, "F", 10, 10)]
        public void GivenCarCommandForword_Validate_carMoveForword(string name, int xCord, int yCord, Direction Direction, string command, int fieldWidth, int fieldHeight)
        {
            // Arrange
            var car = SimulatorServiceHelper.GetCar(name, xCord, yCord, Direction, command, false);
            var field = new FieldDto { Height = fieldHeight, Width = fieldWidth };
            var mapper = Substitute.For<IMapper>();
            mapper.Map<EntityCar>(car).Returns(new EntityCar { Direction = Direction, Name = name, XCoordinate = xCord, YCoordinate = yCord, Command = command });
            var slut = new SimulatorRepository(mapper);
            slut.AddCar(car);

            //Act
            slut.RunCommand(car, field);
            var res = slut.GetAllCar();

            //Assertion
            Assert.Single(res);
            Assert.Equal(1, res[0].XCoordinate);
            Assert.Equal(1, res[0].YCoordinate);
            Assert.Equal(car.Direction, res[0].Direction);


        }
    }
}
