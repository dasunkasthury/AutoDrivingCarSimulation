using AutoDrivingCarSimulator.Core.Enums;

namespace AutoDrivingCarSimulator.Domain.Entity
{
    public class EntityCar
    {
        public required string Name { get; set; }
        public required int XCoordinate { get; set; }
        public required int YCoordinate { get; set; }
        public required Direction Direction { get; set; }
        public string? Command { get; set; }
    }
}
