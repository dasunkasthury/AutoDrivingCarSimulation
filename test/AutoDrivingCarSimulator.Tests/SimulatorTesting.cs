using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Core.Services.Concretes;
using AutoFixture.Xunit2;
using FluentAssertions;

namespace AutoDrivingCarSimulator.Tests;

public class SimulatorTesting
{
    [Theory, InlineAutoData(5, 10)]
    public void GivenFieldCoordinates_Validate_Coordinates(int width, int height)
    {
        // Arrange
        var slut = new SimulatorService();

        //Act
        var res = slut.IsValidField(width, height);

        //Assertion
        res.Should().BeTrue("because the coordinates are within the defined field dimensions");
    }

    [Theory, InlineAutoData(0, 10)]
    public void GivenInvalidFieldCoordinates_Validate_Coordinates(int width, int height)
    {
        // Arrange
        var slut = new SimulatorService();

        //Act
        var res = slut.IsValidField(width, height);

        //Assertion
        res.Should().BeFalse("because this field has no width");
    }

    [Theory, InlineAutoData("A",0,2,Direction.W)]
    public void GivenCarDetails_Validate_CarDetails(string name, int xCord, int yCord, Direction Direction)
    {
        // Arrange
        var car = new CarDto { Name= name, XCoordinate = xCord, YCoordinate = yCord, Direction = Direction }; 
        var slut = new SimulatorService();

        //Act
        var res = slut.IsValidCar(car);

        //Assertion
        res.Should().BeTrue("because the car details are valid");

    }

    [Theory, InlineAutoData("A", 0, -1, Direction.W)]
    public void GivenInvalidCarDetails_Validate_CarDetails(string name, int xCord, int yCord, Direction Direction)
    {
        // Arrange
        var car = new CarDto { Name = name, XCoordinate = xCord, YCoordinate = yCord, Direction = Direction };
        var slut = new SimulatorService();

        //Act
        var res = slut.IsValidCar(car);

        //Assertion
        res.Should().BeFalse("because the car details are valid");

    }

    [Theory, InlineAutoData("A")]
    public void GivenCarName_Validate_CarName(int name)
    {

    }



    [Theory, InlineAutoData("FFRRLL")]
    public void GivenCommand_Validate_Command(string command)
    {

    }

    public void GivenAddNewCar_Validate_CarList(CarDto car)
    {

    }

    public void GivenCarDetails_Validate_CarDestination(CarDto car, FieldDto field)
    {


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
