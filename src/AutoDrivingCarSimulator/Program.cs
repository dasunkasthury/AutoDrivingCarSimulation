// See https://aka.ms/new-console-template for more information

using AutoDrivingCarSimulator;
using AutoDrivingCarSimulator.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;


var serviceProvider = new ServiceCollection()
    .ConfigureServices()
    .BuildServiceProvider();

var app = serviceProvider.GetRequiredService<AutoDrivingCarApp>();
app.Begin();
