using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Core.Interfaces;
using AutoDrivingCarSimulator.Core.Services.Concretes;
using AutoDrivingCarSimulator.Infrastructure.Repositories;
using AutoDrivingCarSimulator.Tests.Helpers;
using AutoFixture.Xunit2;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrivingCarSimulator.Tests
{
    public class SimulatorRepositoryTesting
    {
        [Theory, InlineAutoData("A", 0, 1, Direction.W, "FFLLFR", 10, 10)]
        public void GivenCarDetails_Validate_CarDestination(string name, int xCord, int yCord, Direction Direction, string command, int fieldWidth, int fieldHeight)
        {
            // Arrange
            var car = new CarDto { Name = name, XCoordinate = xCord, YCoordinate = yCord, Direction = Direction, CommandList= command.Select(c => Enum.Parse<Command>(c.ToString())).ToList() };
            var field = new FieldDto { Height=fieldHeight, Width=fieldWidth };
            //var simulator = Substitute.For<ISimulatorRepository>();
            //simulator.GetAllCar().Returns(new List<CarDto> { SimulatorServiceHelper.GetCar(name, xCord, yCord, Direction) });

            var slut = new SimulatorRepository();

            slut.AddCar(car);



            slut.RunCommand(car, field);

            //Act
            var res = slut.GetAllCar();

            //Assertion
            Assert.Single(res);
            Assert.Equal(car.Name, res[0].Name);
            Assert.Equal(car.XCoordinate, res[0].XCoordinate);
            Assert.Equal(car.YCoordinate, res[0].YCoordinate);
            Assert.Equal(car.Direction, res[0].Direction);


        }
    }
}
