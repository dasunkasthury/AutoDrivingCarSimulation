using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrivingCarSimulator.Core.DTO
{
    public record CordinateDto
    {
        public required int XCoordinate { get; init; }
        public required int YCoordinate { get; init; }
    }
}
