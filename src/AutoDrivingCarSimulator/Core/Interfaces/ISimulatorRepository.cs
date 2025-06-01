using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Domain.Entity;

namespace AutoDrivingCarSimulator.Core.Interfaces
{
    public interface ISimulatorRepository
    {
        void AddCar(CarDto car);

        void AddField(FieldDto field);

        FieldDto GetField();

        void RunCommand(CarDto car, FieldDto field);

        void CheckCollision();

        IEnumerable<CarDto> GetCollidedCars();

        IEnumerable<CarDto> GetCompletedCars();

        void ClearData();

        IList<CarDto> GetAllCar();

    }
}
