using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Core.Profiles;
using AutoDrivingCarSimulator.Infrastructure.Repositories;
using AutoDrivingCarSimulator.Tests.Helpers;
using AutoFixture.Xunit2;
using AutoMapper;

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
            Assert.Equal(xCord - 1, res[0].XCoordinate);
            Assert.Equal(yCord, res[0].YCoordinate);
            Assert.Equal(car.Direction, res[0].Direction);
        }

        [Theory, InlineAutoData("A", 0, 0, Direction.N, "R", 10, 10)]
        public void GivenCarCommandTurnRight_Validate_carTurnRight(string name, int xCord, int yCord, Direction Direction, string command, int fieldWidth, int fieldHeight)
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
            Assert.Equal(xCord, res[0].XCoordinate);
            Assert.Equal(yCord, res[0].YCoordinate);
            Assert.Equal(Direction.E, res[0].Direction);
        }

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
            Assert.Single(res);
            Assert.False(res.First().IsCollide);
        }

        [Theory]
        [ InlineAutoData("A", 0, 0, Direction.N, "R")]
        [ InlineAutoData("B", 0, 0, Direction.N, "R")]
        public void GivenCars_validate_collision(string name, int xCord, int yCord, Direction Direction, string command)
        {
            // Arrange
            var car = SimulatorServiceHelper.GetCar(name, xCord, yCord, Direction, command, false);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarProfile>());
            var mapper = config.CreateMapper();

            var slut = new SimulatorRepository(mapper);
            slut.AddCar(car);

            //Act
            slut.CheckCollision();
            var res = slut.GetAllCar();

            //Assertion
            Assert.Single(res);
            Assert.False(res.First().IsCollide);
        }
    }
}
