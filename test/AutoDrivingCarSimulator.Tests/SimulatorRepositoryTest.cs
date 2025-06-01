using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Core.Profiles;
using AutoDrivingCarSimulator.Infrastructure.Repositories;
using AutoDrivingCarSimulator.Tests.Helpers;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using System.Linq;

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
            var car1 = SimulatorServiceHelper.GetCar("B", xCord, yCord, Direction, command, false);
            var car2 = SimulatorServiceHelper.GetCar(name, xCord, yCord, Direction, command, false);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarProfile>());
            var mapper = config.CreateMapper();

            var slut = new SimulatorRepository(mapper);
            slut.AddCar(car1);
            slut.AddCar(car2);

            //Act
            slut.CheckCollision();
            var res = slut.GetAllCar();

            //Assertion
            res.Should().HaveCount(2);
            res.Should().OnlyContain(c=>c.IsCollide);
        }

        [Theory]
        [InlineAutoData("A", 0, 0, Direction.N, "F", 10, 10)]
        [InlineAutoData("B", 5, 7, Direction.N, "F", 10, 10)]
        public void GivenCarCollided_validate_carShouldNotMove(string name, int xCord, int yCord, Direction Direction, string command, int fieldWidth, int fieldHeight)
        {
            // Arrange
            var car = SimulatorServiceHelper.GetCar("B", xCord, yCord, Direction, command, true);
            var field = new FieldDto { Height = fieldHeight, Width = fieldWidth };

            var config = new MapperConfiguration(cfg => cfg.AddProfile<CarProfile>());
            var mapper = config.CreateMapper();

            var slut = new SimulatorRepository(mapper);
            slut.AddCar(car);

            //Act
            slut.RunCommand(car, field);
            var res = slut.GetAllCar();

            //Assertion
            res.Should().HaveCount(1);
            res.First().XCoordinate.Should().Be(xCord);
            res.First().YCoordinate.Should().Be(yCord);
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
            Assert.Single(res);
            Assert.True(res.First().IsCollide);
        }
    }
}
