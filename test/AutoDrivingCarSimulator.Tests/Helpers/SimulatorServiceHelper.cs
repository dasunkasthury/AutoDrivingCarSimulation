using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;

namespace AutoDrivingCarSimulator.Tests.Helpers
{
    public static class SimulatorServiceHelper
    {
        public static List<CarDto> GetCarList()
        {
            return new List<CarDto>
            {
                new() {Direction = Direction.N, Name = "A", XCoordinate = 0 , YCoordinate = 10},
                new() {Direction = Direction.S, Name = "B", XCoordinate = 5 , YCoordinate = 7}
            };
        }

        public static CarDto GetCar(string name, int xCord, int yCord, Direction direction)
        {
            return new CarDto { Direction = direction, Name = name, XCoordinate = xCord, YCoordinate = yCord };
        }
    }
}
