using AutoDrivingCarSimulator.Core.DTO;

namespace AutoDrivingCarSimulator.Core.Services.Concretes
{
    public class SimulatorService : ISimulatorService
    {
        private int _width;
        private int _height;

        public SimulatorService()
        {
        }

        public void AddCar(CarDto car)
        {
            throw new NotImplementedException();
        }

        public void AddField(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public bool IsValidCar(CarDto car)
        {
            return car.XCoordinate >= 0 && car.YCoordinate >= 0;
        }

        public bool IsValidCommand(string command)
        {
            throw new NotImplementedException();
        }

        public bool IsValidField(int width, int height)
        {
            return width > 0 && height > 0;
        }
    }
}
