namespace AutoDrivingCarSimulator.Core.Services.Concretes
{
    public class SimulatorService : ISimulatorService
    {
        private int _width;
        private int _height;

        public SimulatorService()
        {
        }
        public void AddField(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public bool IsValidField(int width, int height)
        {
            return width > 0 && height > 0;
        }
    }
}
