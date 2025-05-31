using AutoDrivingCarSimulator.Core.Enums;

namespace AutoDrivingCarSimulator.Core.DTO
{
    public record CarDto : CordinateDto
    {
        public required string Name { get; init; }
        public required Direction Direction { get; init; }
        public IList<Command>? CommandList { get; set; }
        public int CompletedSteps { get; set; }
        public bool IsCollide { get; set; }
    }
}
