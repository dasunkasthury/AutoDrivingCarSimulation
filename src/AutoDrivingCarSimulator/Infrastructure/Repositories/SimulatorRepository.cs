using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Interfaces;
using AutoDrivingCarSimulator.Domain.Entity;
using AutoMapper;

namespace AutoDrivingCarSimulator.Infrastructure.Repositories
{
    public class SimulatorRepository : ISimulatorRepository
    {
        private readonly List<EntityCar> _cars = new(); // Fix for IDE0090: 'new' expression can be simplified
        private readonly IMapper _mapper;
        public void AddCar(CarDto car)
        {
            _cars.Add(_mapper.Map<EntityCar>(car));
        }

        public void AddField(FieldDto field)
        {
            throw new NotImplementedException();
        }

        public void CheckCollision(CarDto car)
        {
            throw new NotImplementedException();
        }

        public void ClearData()
        {
            throw new NotImplementedException();
        }

        public IList<CarDto> GetAllCar()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CarDto> GetCollidedCars()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CarDto> GetCompletedCars()
        {
            throw new NotImplementedException();
        }

        public FieldDto GetField()
        {
            throw new NotImplementedException();
        }

        public void RunCommand(CarDto car, FieldDto field)
        {
            throw new NotImplementedException();
        }
    }
}
