using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Core.Interfaces;
using AutoDrivingCarSimulator.Core.Services.Concretes;
using AutoDrivingCarSimulator.Tests.Helpers;
using AutoFixture.Xunit2;
using FluentAssertions;
using NSubstitute;
using System.Xml.Linq;
using Xunit;

namespace AutoDrivingCarSimulator.Tests;

public class SimulatorTest
{
    [Theory, InlineAutoData(5, 10)]
    public void GivenFieldCoordinates_Validate_Coordinates(int width, int height)
    {
        // Arrange
        var simulator = Substitute.For<ISimulatorRepository>();
        var slut = new SimulatorService(simulator);

        //Act
        var res = slut.IsValidField(width, height);

        //Assertion
        res.Should().BeTrue("because the coordinates are within the defined field dimensions");
    }

    [Theory, InlineAutoData(0, 10)]
    public void GivenInvalidFieldCoordinates_Validate_Coordinates(int width, int height)
    {
        // Arrange
        var simulator = Substitute.For<ISimulatorRepository>();
        var slut = new SimulatorService(simulator);

        //Act
        var res = slut.IsValidField(width, height);

        //Assertion
        res.Should().BeFalse("because this field has no width");
    }

    [Theory, InlineAutoData("A",0,2,Direction.W)]
    public void GivenCarDetails_Validate_CarDetails(string name, int xCord, int yCord, Direction direction)
    {
        // Arrange
        var car = new CarDto { Name= name, XCoordinate = xCord, YCoordinate = yCord, Direction = direction };
        var simulator = Substitute.For<ISimulatorRepository>();
        var slut = new SimulatorService(simulator);

        //Act
        var res = slut.IsValidCar(car);

        //Assertion
        res.Should().BeTrue("because the car details are valid");

    }

    [Theory, InlineAutoData("A", 0, -1, Direction.W)]
    public void GivenInvalidCarDetails_Validate_CarDetails(string name, int xCord, int yCord, Direction direction)
    {
        // Arrange
        var car = new CarDto { Name = name, XCoordinate = xCord, YCoordinate = yCord, Direction = direction };
        var simulator = Substitute.For<ISimulatorRepository>();
        var slut = new SimulatorService(simulator);

        //Act
        var res = slut.IsValidCar(car);

        //Assertion
        res.Should().BeFalse("because the car details are valid");

    }

    [Theory, InlineAutoData("A")]
    public void GivenInvalidCarName_Validate_CarName(string name)
    {
        // Arrange
        var simulator = Substitute.For<ISimulatorRepository>();
        simulator.GetAllCar().Returns(SimulatorServiceHelper.GetCarList());
        var slut = new SimulatorService(simulator);

        //Act
        var res = slut.IsValidCarName(name);

        //Assertion
        res.Should().BeFalse("because Car already exisits with this name");
    }

    [Theory, InlineAutoData("C")]
    public void GivenCarName_Validate_CarName(string name)
    {
        // Arrange
        var simulator = Substitute.For<ISimulatorRepository>();
        simulator.GetAllCar().Returns(SimulatorServiceHelper.GetCarList());
        var slut = new SimulatorService(simulator);

        //Act
        var res = slut.IsValidCarName(name);

        //Assertion
        res.Should().BeTrue("because no Car exisits with this name");
    }

    [Theory, InlineAutoData("FFRRLL")]
    public void GivenCommand_Validate_Command(string command)
    {
        // Arrange
        var simulator = Substitute.For<ISimulatorRepository>();
        var slut = new SimulatorService(simulator);

        //Act
        var res = slut.IsValidCommand(command);

        //Assertion
        res.Should().BeTrue("because the given command contain only valid commands");

    }

    [Theory, InlineAutoData("FFRRLLAA")]
    public void GivenInvalidCommand_Validate_Command(string command)
    {
        // Arrange
        var simulator = Substitute.For<ISimulatorRepository>();
        var slut = new SimulatorService(simulator);

        //Act
        var res = slut.IsValidCommand(command);

        //Assertion
        res.Should().BeFalse("because the given command contain unknown commands");

    }

    [Theory, InlineAutoData("A", 0, 1, Direction.W)]
    public void GivenAddNewCar_Validate_CarList(string name, int xCord, int yCord, Direction direction)
    {
        // Arrange
        var car = new CarDto { Name = name, XCoordinate = xCord, YCoordinate = yCord, Direction = direction };
        var simulator = Substitute.For<ISimulatorRepository>();
        simulator.GetAllCar().Returns(new List<CarDto> { SimulatorServiceHelper.GetCar(name, xCord, yCord, direction) });
        var slut = new SimulatorService(simulator);
        slut.AddCar(car);

        //Act
        var res = slut.GetAllCars();

        //Assertion
        Assert.Single(res);
        Assert.Equal(car.Name, res[0].Name);
        Assert.Equal(car.XCoordinate, res[0].XCoordinate);
        Assert.Equal(car.YCoordinate, res[0].YCoordinate);
        Assert.Equal(car.Direction, res[0].Direction);
    }

    [Theory, InlineAutoData("A", 0, 1, Direction.W, "FFLLR", 10, 10)]
    public void GivenCarDetails_Validate_CarDestination(string name, int xCord, int yCord, Direction direction, string command, int fieldWidth, int fieldHeight)
    {
        // Arrange
        var car = new CarDto { Name = name, XCoordinate = xCord, YCoordinate = yCord, Direction = direction };
        var simulator = Substitute.For<ISimulatorRepository>();
        simulator.GetAllCar().Returns(new List<CarDto> { SimulatorServiceHelper.GetCar(name, xCord, yCord, direction, command) });
        simulator.GetField().Returns(new FieldDto { Height = fieldHeight, Width = fieldWidth });
        var slut = new SimulatorService(simulator);
        slut.FindDestination();

        //Act
        var res = slut.GetAllCars();

        //Assertion
        Assert.Single(res);
    }

    [Theory]
    [InlineAutoData("A", 0, 1, Direction.W, "FFLLR")]
    [InlineAutoData("B", 5, 8, Direction.N, "FF")]
    public void GivenFinalDestination_Validate_CompletedResults(string name, int xCord, int yCord, Direction direction, string command)
    {
        // Arrange
        var car = new CarDto { Name = name, XCoordinate = xCord, YCoordinate = yCord, Direction = direction };
        var simulator = Substitute.For<ISimulatorRepository>();
        simulator.GetCompletedCars().Returns(new List<CarDto> { SimulatorServiceHelper.GetCar(name, xCord, yCord, direction, command, false) });

        var slut = new SimulatorService(simulator);

        //Act
        var res = slut.GetResults();

        //Assertion
        res.Should().NotBeNull();
        res.Should().HaveCount(1);
        res.Should().Contain(x=>x.Equals($"{name}, ({xCord},{yCord})  {direction}"));
    }

    [Theory]
    [InlineAutoData("A", 0, 1, Direction.W, "FFLLR")]
    [InlineAutoData("A", 8, 0, Direction.S, "FF")]
    public void GivenFinalDestination_Validate_CollidedResults(string name, int xCord, int yCord, Direction direction, string command)
    {
        // Arrange
        var car = new CarDto { Name = name, XCoordinate = xCord, YCoordinate = yCord, Direction = direction };
        var simulator = Substitute.For<ISimulatorRepository>();
        simulator.GetCollidedCars().Returns(new List<CarDto> { SimulatorServiceHelper.GetCar(name, xCord, yCord, direction, command, true) });

        var slut = new SimulatorService(simulator);

        //Act
        var res = slut.GetResults();

        //Assertion
        res.Should().NotBeNull();
        res.Should().HaveCount(1);
        res.Should().Contain(x => x.Equals($"{name}, collides with  at ({xCord},{yCord}) at step 0"));
    }

    public void GivenCarDetails_Validate_CarCollition(IList<CarDto> carList, FieldDto field)
    {


    }


    public void GivenExit_Validate_AppStatus()
    {


    }

    public void GivenAddCarOption_Validate_AppNextStep()
    {


    }

    public void GivenCarDetails_beyondBoundary_Validate_CarDestination(CarDto car, FieldDto field)
    {


    }

}
