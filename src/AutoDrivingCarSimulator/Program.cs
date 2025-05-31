// See https://aka.ms/new-console-template for more information


using AutoDrivingCarSimulator.Core.DTO;
using AutoDrivingCarSimulator.Core.Enums;
using AutoDrivingCarSimulator.Core.Services;
using AutoDrivingCarSimulator.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .ConfigureServices()
    .BuildServiceProvider();

var simulationService = serviceProvider.GetRequiredService<ISimulatorService>();

Begin();
void Begin()
{
    Console.WriteLine("Welcome to Auto Driving Car Simulation!");
    Console.WriteLine("Please enter the width and height of the simulation field in x y format:");

    string[] coordinates = Console.ReadLine().Split(' ');
    int width = int.Parse(coordinates[0]);
    int height = int.Parse(coordinates[1]);

    // Initialize the simulation field
    simulationService.AddField(width, height);

    while (true)
    {
        Console.WriteLine($"You have created a field of {width} x {height}.");
        Console.WriteLine("Please choose from the following options:");
        Console.WriteLine("[1] Add a car to field");
        Console.WriteLine("[2] Run simulation");
        Console.WriteLine("[Q] Quit");
        Console.Write("> ");

        var input = Console.ReadLine()?.ToUpper();

        switch (input)
        {
            case "1":
                AddCar();
                break;
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
    CarDto car;

    do
    {
        Console.WriteLine("Please enter the name of the car:");
        name = Console.ReadLine();

        isValidName = simulationService.IsValidCarName(name);

        if (!isValidName)
        {
            Console.WriteLine("You have entered an invalid name");
        }
    } while (!isValidName);


    do
    {
        Console.WriteLine($"Please enter initial position of car {name} in x y Direction format:");
        string[] position = Console.ReadLine().Split(' ');
        int x = int.Parse(position[0]);
        int y = int.Parse(position[1]);
        Direction direction = Enum.Parse<Direction>(position[2]);

        car = new CarDto()
        {
            Name = name,
            XCoordinate = x,
            YCoordinate = y,
            Direction = direction
        };

        isValidCar = simulationService.IsValidCar(car);

        if (!isValidCar)
        {
            Console.WriteLine("You have entered an invalid car details");
        }
    } while (!isValidCar);

    do
    {
        Console.WriteLine($"Please enter the commands for car {name}:");
        string command = Console.ReadLine();

        isValidCommand = simulationService.IsValidCommand(command.ToUpper());

        car.CommandList = command.ToUpper().Select(c => Enum.Parse<Command>(c.ToString())).ToList();

        if (!isValidCommand)
        {
            Console.WriteLine("You have entered an invalid command");
        }

    } while (!isValidCommand);

    simulationService.AddCar(car);

    Console.WriteLine("Your current list of cars are:");
    foreach (var c in simulationService.GetAllCars())
    {
        Console.WriteLine($"- {c.Name}, ({c.XCoordinate},{c.YCoordinate}) {c.Direction}, {string.Join("", c.CommandList)}");
    }
}