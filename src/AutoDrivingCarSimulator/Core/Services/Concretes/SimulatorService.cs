using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Interfaces;
using AutoDrivingCarSimulator.Infrastructure.Repositories;

namespace AutoDrivingCarSimulator.Core.Services.Concretes
{
    public class SimulatorService : ISimulatorService
    {
        private readonly ISimulatorRepository _simulatorRepository;

        public SimulatorService(ISimulatorRepository simulatorRepository)
        {
            _simulatorRepository = simulatorRepository;
        }

        public void AddCar(CarDto car)
        {
            _simulatorRepository.AddCar(car);
        }

        public void AddField(int width, int height)
        {
            _simulatorRepository.AddField(new FieldDto { Height = height, Width = width });
        }

        public void FindDestination()
        {
            throw new NotImplementedException();
        }

        public IList<CarDto> GetAllCars()
        {
            var existingCar = _simulatorRepository.GetAllCar();
            return existingCar;
        }

        public bool IsValidCar(CarDto car)
        {
            return car.XCoordinate >= 0 && car.YCoordinate >= 0;
        }

        public bool IsValidCarName(string name)
        {
            var existingCar = _simulatorRepository.GetAllCar();
            return !string.IsNullOrEmpty(name) && !existingCar.Any(car => car.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsValidCommand(string command)
        {
            return command.All(c => c == 'L' || c == 'R' || c == 'F');
        }

        public bool IsValidField(int width, int height)
        {
            return width > 0 && height > 0;
        }
    }
}
