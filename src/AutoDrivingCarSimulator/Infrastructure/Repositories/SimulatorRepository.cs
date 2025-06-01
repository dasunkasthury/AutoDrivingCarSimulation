using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Interfaces;
using AutoDrivingCarSimulator.Domain.Entity;
using AutoMapper;

namespace AutoDrivingCarSimulator.Infrastructure.Repositories
{
    public class SimulatorRepository : ISimulatorRepository
    {
        private List<EntityCar> _cars = new();
        private readonly IMapper _mapper;
        private EntityField _field;

        public SimulatorRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void AddCar(CarDto car)
        {
            _cars.Add(_mapper.Map<EntityCar>(car));
        }

        public void AddField(FieldDto field)
        {
            _field = _mapper.Map<EntityField>(field);
        }

        public void ClearData()
        {
            throw new NotImplementedException();
        }

        public IList<CarDto> GetAllCar()
        {
            return _mapper.Map<List<CarDto>>(_cars);
        }

        public IList<EntityCar> GetAllCarEntities()
        {
            return _cars;
        }

        public IEnumerable<CarDto> GetCollidedCars()
        {
            return _mapper.Map<IEnumerable<CarDto>>(_cars.Where(c => c.IsCollide));
        }

        public IEnumerable<CarDto> GetCompletedCars()
        {
            return _mapper.Map<IEnumerable<CarDto>>(_cars.Where(c => !c.IsCollide));
        }

        public FieldDto GetField()
        {
            return _mapper.Map<FieldDto>(_field);
        }

        public void UpdateCarList(IEnumerable<EntityCar> cars)
        {
            _cars = cars.ToList();
        }
    }
}
