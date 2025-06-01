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
            var field = _simulatorRepository.GetField();

            bool hasMoreCommands = true;
            do
            {
                var carList = _simulatorRepository.GetAllCar();

                foreach (var car in carList)
                {
                    _simulatorRepository.RunCommand(car, field);
                }
                hasMoreCommands = carList.Any(c => c.CommandList.Count > 0); // Check if any car has commands left

            } while (hasMoreCommands);
        }

        public IList<CarDto> GetAllCars()
        {
            var existingCar = _simulatorRepository.GetAllCar();
            return existingCar;
        }

        public IEnumerable<string> GetResults()
        {
            IList<string> result = new List<string>();
            var completedCarList = _simulatorRepository.GetCompletedCars();

            foreach (var completedcar in completedCarList)
            {
                result.Add($"{completedcar.Name}, ({completedcar.XCoordinate},{completedcar.YCoordinate})  {completedcar.Direction}");
            }

            return result;
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
