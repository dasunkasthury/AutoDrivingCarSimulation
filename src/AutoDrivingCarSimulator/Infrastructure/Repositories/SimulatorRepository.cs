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
            var cars = _mapper.Map<List<CarDto>>(_cars);
            return cars;
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
            var selectedCar = _cars.FirstOrDefault(c => c.Name == car.Name);
            if (selectedCar.Command.Length > 0 && !selectedCar.IsCollide)
            {
                var cmd = selectedCar.Command[0];

                selectedCar.Command = selectedCar.Command.Substring(1);// Remove the executed commands to avoid getting execute them again

                switch (cmd)
                {
                    case 'L':
                        // Logic to turn left
                        selectedCar.TurnLeft();
                        break;
                    case 'R':
                        // Logic to turn right
                        selectedCar.TurnRight();
                        break;
                    case 'F':
                        // Logic to move forward
                        selectedCar.MoveForward(field);
                        break;
                }
            }
        }
    }
}
