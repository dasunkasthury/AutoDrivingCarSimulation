using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrivingCarSimulator.Core.Services
{
    public interface ISimulatorService
    {
        void AddField(int width, int height);
        bool IsValidField(int width, int height);
    }
}
