using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Domain.Entity;

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

        public static CarDto GetCar(string name, int xCord, int yCord, Direction direction, string command)
        {
            return new CarDto { Direction = direction, Name = name, XCoordinate = xCord, YCoordinate = yCord, CommandList = command.Select(c => Enum.Parse<Command>(c.ToString())).ToList() };
        }

        public static CarDto GetCar(string name, int xCord, int yCord, Direction direction, string command, bool isCollide)
        {
            return new CarDto { Direction = direction, Name = name, XCoordinate = xCord, YCoordinate = yCord, CommandList = command.Select(c => Enum.Parse<Command>(c.ToString())).ToList(), IsCollide = isCollide };
        }

        public static EntityCar GetEntityCar(string name, int xCord, int yCord, Direction direction, string command, bool isCollide)
        {
            return new EntityCar { Direction = direction, Name = name, XCoordinate = xCord, YCoordinate = yCord, Command = command, IsCollide = isCollide };
        }
    }
}
