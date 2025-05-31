// See https://aka.ms/new-console-template for more information


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

}