using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Domain.Entity;

namespace AutoDrivingCarSimulator.Core.Interfaces
{
    public interface ISimulatorRepository
    {
        void AddCar(CarDto car);

        void AddField(FieldDto field);

        FieldDto GetField();

        IEnumerable<CarDto> GetCollidedCars();

        IEnumerable<CarDto> GetCompletedCars();

        void ClearData();

        IList<CarDto> GetAllCar();

        IList<EntityCar> GetAllCarEntities();

        void UpdateCarList (IEnumerable<EntityCar> cars);

    }
}
