using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Interfaces;
using AutoDrivingCarSimulator.Domain.Entity;

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


        public IList<EntityCar> CalculateDestination(FieldDto field, IList<EntityCar> CarEntityList) // this is calculating the destination by going through all the commands for each car
        {
            bool hasMoreCommands = true;
            do
            {
                foreach (var car in CarEntityList)
                {
                    if (car.Command.Length > 0 && !car.IsCollide)
                    {
                        var cmd = car.Command[0]; // this is the next command to be executed for each car

                        car.Command = car.Command.Substring(1);// Remove the first command. this is to avoid getting execute this command again.  

                        switch (cmd)
                        {
                            case 'L':
                                // Logic to turn left
                                car.TurnLeft();
                                break;
                            case 'R':
                                // Logic to turn right
                                car.TurnRight();
                                break;
                            case 'F':
                                // Logic to move forward
                                car.MoveForward(field);
                                break;
                        }
                    }
                    CheckCollision(CarEntityList); // this is to check for the collisions after executing each command for each car. This is to determind the collition ASAP
                }
                hasMoreCommands = CarEntityList.Any(c => c.Command.Length > 0 && !c.IsCollide); // Check if any car which is not collided and has commands left

            } while (hasMoreCommands);

            return CarEntityList;
        }


        public void UpdateDestination() // this is responsible to codinate the command execution and update the results using infrastracture layer
        {
            var field = _simulatorRepository.GetField();
            var carList = _simulatorRepository.GetAllCarEntities();
            var calculatedCarList = CalculateDestination(field, carList);
            _simulatorRepository.UpdateCarList(calculatedCarList);
        }

        public void CheckCollision(IList<EntityCar> carList)
        {
            var isCollideCar = carList.GroupBy(c => new { c.YCoordinate, c.XCoordinate }).Where(g => g.Count() > 1).Select(c => c.Key).ToList(); // to check whether is there any multiple cars with the same cordinates

            if (isCollideCar.Any())
            {
                foreach (var collideCar in carList.Where(c => c.YCoordinate == isCollideCar[0].YCoordinate && c.XCoordinate == isCollideCar[0].XCoordinate).ToList()) // this is to update every car that has been collided
                {
                    collideCar.IsCollide = true;
                }
            }
        }

        public IList<CarDto> GetAllCars()
        {
            var existingCar = _simulatorRepository.GetAllCar();
            return existingCar;
        }

        public IEnumerable<string> GetResults() // this is to get the final results considering both completed and collided cars
        {
            IList<string> result = new List<string>();
            var completedCarList = _simulatorRepository.GetCompletedCars();
            var collidedCarList = _simulatorRepository.GetCollidedCars();

            if (collidedCarList.Any())
            {
                foreach (var collidedcar in collidedCarList)
                {
                    result.Add($"{collidedcar.Name}, collides with {string.Join(", ", collidedCarList.Where(c => c.Name != collidedcar.Name).Select(c => c.Name))} at ({collidedcar.XCoordinate},{collidedcar.YCoordinate}) at step {collidedcar.CompletedSteps}");
                }

            }

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

        public void reset()
        {
            _simulatorRepository.ClearData();
        }

        public bool IsAnyCarAvailable()
        {
            return _simulatorRepository.GetAllCar().Any();
        }
    }
}
