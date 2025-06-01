namespace AutoDrivingCarSimulator.Core.DTO
{
    public record CordinateDto
    {
        public required int XCoordinate { get; init; }
        public required int YCoordinate { get; init; }
    }
}
