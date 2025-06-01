using AutoDrivingCarSimulator.Core.DTO;
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
        public bool IsCollide { get; set; }
        public int CompletedSteps { get; set; } = 0; 

        public void MoveForward(FieldDto field)
        {
            CompletedSteps++;
            switch (Direction)
            {
                case Direction.N:
                    YCoordinate = YCoordinate < field.Height ? YCoordinate + 1 : YCoordinate; // to handle the car going beyond the height of thr field
                    break;
                case Direction.E:
                    XCoordinate = XCoordinate < field.Width ? XCoordinate + 1 : XCoordinate; // to handle the car going beyond the width of the field
                    break;
                case Direction.S:
                    YCoordinate = YCoordinate > 0 ? YCoordinate - 1 : YCoordinate; // to handle the car going lower than the field
                    break;
                case Direction.W:
                    XCoordinate = XCoordinate > 0 ? XCoordinate - 1 : XCoordinate; // to handle the car going lower than the field
                    break;
            }
        }

        public void TurnRight()
        {
            CompletedSteps++;
            switch (Direction)
            {
                case Direction.N:
                    Direction = Direction.E;
                    break;
                case Direction.E:
                    Direction = Direction.S;
                    break;
                case Direction.S:
                    Direction = Direction.W;
                    break;
                case Direction.W:
                    Direction = Direction.N;
                    break;

            }
        }

        public void TurnLeft()
        {
            CompletedSteps++;
            switch (Direction)
            {
                case Direction.N:
                    Direction = Direction.W;
                    break;
                case Direction.E:
                    Direction = Direction.N;
                    break;
                case Direction.S:
                    Direction = Direction.E;
                    break;
                case Direction.W:
                    Direction = Direction.S;
                    break;

            }

        }
    }
}
