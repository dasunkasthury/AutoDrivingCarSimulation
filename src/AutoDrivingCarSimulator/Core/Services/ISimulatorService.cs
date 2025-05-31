using AutoDrivingCarSimulator.Core.DTO;

namespace AutoDrivingCarSimulator.Core.Services
{
    public interface ISimulatorService
    {
        void AddField(int width, int height);
        bool IsValidField(int width, int height);
        bool IsValidCar(CarDto car);
        void AddCar(CarDto car);
        bool IsValidCommand(string command);
    }
}
