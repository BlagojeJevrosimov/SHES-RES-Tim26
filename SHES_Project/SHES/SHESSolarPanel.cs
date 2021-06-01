using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SHES
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SHESSolarPanel : ISHESSolarPanel
    {
        public static double bufferPowerOutput;

        public void SendData(double powerOutput)
        {
            if (powerOutput < 0)
                throw new ArgumentOutOfRangeException("Snaga solarnih panela ne moze biti negativna!");
            else
                bufferPowerOutput = powerOutput;
        }
    }
}
