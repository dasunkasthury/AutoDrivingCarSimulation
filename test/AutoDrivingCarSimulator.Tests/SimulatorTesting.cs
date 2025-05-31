using AutoFixture.Xunit2;

namespace AutoDrivingCarSimulator.Tests;

public class SimulatorTesting
{
    [Theory, InlineAutoData(5, 10)]
    public void GivenFieldCoordinates_Validate_Coordinates(int width, int height)
    {

    }

    [Theory, InlineAutoData(5, 10)]
    public void GivenCarDetails_Validate_CarDetails(CarDto car)
    {

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
