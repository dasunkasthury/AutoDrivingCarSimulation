using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Core.Services;

namespace AutoDrivingCarSimulator
{
    public class AutoDrivingCarApp // moved the content of program.cs file to this servic
    {
        private readonly ISimulatorService _simulatorService;

        public AutoDrivingCarApp(ISimulatorService simulatorService)
        {
            _simulatorService = simulatorService;
        }
        public void Begin()
        {
            Console.WriteLine("Welcome to Auto Driving Car Simulation!");
            bool isValidField = true;
            int width = 0;
            int height = 0;
            do
            {
                Console.WriteLine("Please enter the width and height of the simulation field in x y format:");

                string[] coordinates = Console.ReadLine().Split(' ');
                isValidField = coordinates.Length == 2;
                if (isValidField)
                {
                    width = int.Parse(coordinates[0]);
                    height = int.Parse(coordinates[1]);
                    isValidField = _simulatorService.IsValidField(width, height);
                }

                else
                {
                    Console.WriteLine("You have entered invalid width and height ");
                }
            } while (!isValidField);

            // Initialize the simulation field
            _simulatorService.AddField(width, height);

            while (true)
            {
                Console.WriteLine($"You have created a field of {width} x {height}.");
                Console.WriteLine("Please choose from the following options:");
                Console.WriteLine("[1] Add a car to field");
                Console.WriteLine("[2] Run simulation");

                var input = Console.ReadLine()?.ToUpper();

                switch (input)
                {
                    case "1":
                        AddCar();
                        break;
                    case "2":
                        Simulate();
                        break;
                    case "Q" or "q":
                        Quit();
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        void AddCar()
        {
            bool isValidName = true;
            string name = "";
            bool isValidCar = true;
            bool isValidCommand = true;
            CarDto car = new CarDto { Direction = Direction.E, Name = "", XCoordinate = 0, YCoordinate = 0 };

            do
            {
                Console.WriteLine("Please enter the name of the car:");
                name = Console.ReadLine();

                isValidName = _simulatorService.IsValidCarName(name);

                if (!isValidName)
                {
                    Console.WriteLine("You have entered an invalid or existing car name");
                }
            } while (!isValidName);

            do
            {
                Console.WriteLine($"Please enter initial position of car {name} in x y Direction format:");
                string[] position = Console.ReadLine().Split(' ');
                isValidCar = position.Length == 3;

                if (isValidCar)
                {
                    int x = int.Parse(position[0]);
                    int y = int.Parse(position[1]);

                    isValidCar = Enum.IsDefined(typeof(Direction), (position[2]));
                    if (!isValidCar)
                    {
                        Console.WriteLine("You have entered an invalid car direction");
                        continue;
                    }

                    car = new CarDto()
                    {
                        Name = name,
                        XCoordinate = x,
                        YCoordinate = y,
                        Direction = Enum.Parse<Direction>(position[2])
                    };
                    isValidCar = _simulatorService.IsValidCar(car);
                }
                else
                {
                    Console.WriteLine("You have entered an invalid car details");
                }
            } while (!isValidCar);

            do
            {
                Console.WriteLine($"Please enter the commands for car {name}:");
                string command = Console.ReadLine();

                isValidCommand = _simulatorService.IsValidCommand(command.ToUpper());

                if (!isValidCommand)
                {
                    Console.WriteLine("You have entered an invalid command");
                    continue;
                }

                car.CommandList = command.ToUpper().Select(c => Enum.Parse<Command>(c.ToString())).ToList();

            } while (!isValidCommand);

            _simulatorService.AddCar(car);

            Console.WriteLine("Your current list of cars are:");
            foreach (var c in _simulatorService.GetAllCars())
            {
                Console.WriteLine($"- {c.Name}, ({c.XCoordinate},{c.YCoordinate}) {c.Direction}, {string.Join("", c.CommandList)}");
            }
        }

        void Simulate()
        {
            if (!_simulatorService.IsAnyCarAvailable()) // to avoid run simulation and print empty values with no car data
            {
                Console.WriteLine("Please enter at least one car befor run simulation");
                Begin();
            }

            Console.WriteLine("Your current list of cars are:");
            foreach (var c in _simulatorService.GetAllCars())
            {
                Console.WriteLine($"- {c.Name}, ({c.XCoordinate},{c.YCoordinate}) {c.Direction}, {string.Join("", c.CommandList)}");
            }

            _simulatorService.UpdateDestination();

            Console.WriteLine("After simulation, the result is:");
            foreach (var result in _simulatorService.GetResults())
            {
                Console.WriteLine(result);
            }

            Console.WriteLine("Please choose from the following options:");
            Console.WriteLine("[1] Start over");
            Console.WriteLine("[2] Exit");

            string userOption = Console.ReadLine();
            if (userOption == "1")
            {
                _simulatorService.reset(); // Reset the simulation
                Begin(); // Restart the simulation
                return;
            }
            else if (userOption == "2")
            {
                Quit();
                return;
            }

        }

        void Quit()
        {
            Console.WriteLine("Thank you for running the simulation. Goodbye!");
        }

    }
}
